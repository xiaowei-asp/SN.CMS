using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Linq;
using System.Text;

namespace SN.CMS.Common.RabbitMq
{
    public class SNRabbitMqClient : ISNRabbitMqClient
    {
        public IConnection Connection { get; }
        public RabbitMqOption Options { get; }

        public SNRabbitMqClient(IConnection connection, IConfiguration configuration)
        {
            this.Connection = connection;
            Options = configuration.GetOptions<RabbitMqOption>("rabbitmq");
        }

        /// <summary>
        /// RabbitMq 发布
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exchangeType"></param>
        public void Publish(string message, string exchangeType = ExchangeType.Fanout)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;

            using var channel = Connection.CreateModel();

            channel.ExchangeDeclare(exchange: Options.Exchange, type: exchangeType);

            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: Options.Exchange,
                routingKey: Options.RoutingKey,
                basicProperties: null,
                body: body);
        }

        /// <summary>
        /// RabbitMq 订阅
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="exchangeType"></param>
        public void Subscribe(Action<string> handler, string exchangeType = ExchangeType.Fanout)
        {
            var channel = Connection.CreateModel();
            channel.ExchangeDeclare(exchange: Options.Exchange, type: exchangeType);

            var queueName = channel.QueueDeclare(Options.QueueName).QueueName;
            channel.QueueBind(queue: queueName,
                exchange: Options.Exchange,
                routingKey: Options.RoutingKey);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body.ToArray());
                handler(message);
            };
            channel.BasicConsume(queue: queueName,
                autoAck: true,
                consumer: consumer);
        }
    }
}
