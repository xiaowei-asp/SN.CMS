using RabbitMQ.Client;
using System;

namespace SN.CMS.Common.RabbitMq
{
    public interface ISNRabbitMqClient
    {
        void Publish(string message, string exchangeType = ExchangeType.Fanout);

        void Subscribe(Action<string> handler, string exchangeType = ExchangeType.Fanout);
    }
}
