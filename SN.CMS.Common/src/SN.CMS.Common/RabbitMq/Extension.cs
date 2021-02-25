using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace SN.CMS.Common.RabbitMq
{
    public static class Extension
    {
        public static void AddRabbitMq(this ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var options = configuration.GetOptions<RabbitMqOption>("rabbitmq");

                return options;
            }).SingleInstance();

            builder.Register(context =>
            {
                var options = context.Resolve<RabbitMqOption>();

                var rabbitFactory = new ConnectionFactory()
                {
                    HostName = options.HostName,
                    Port = options.Port,
                    VirtualHost = options.VirtualHost,
                    UserName = options.UserName,
                    Password = options.Password
                };

                var connect = rabbitFactory.CreateConnection();
                return connect;

            }).SingleInstance();

            builder.RegisterType<SNRabbitMqClient>()
                .As<ISNRabbitMqClient>()
                .InstancePerLifetimeScope();

        }

        
    }
}
