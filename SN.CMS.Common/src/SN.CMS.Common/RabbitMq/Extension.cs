using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace SN.CMS.Common.RabbitMq
{
    public static class Extension
    {
        private static readonly string SectionName = "rabbitmq";

        public static void AddRabbitMq(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }
            var section = configuration.GetSection(SectionName);
            var options = configuration.GetOptions<RabbitMqOption>(SectionName);
            services.Configure<RabbitMqOption>(section);
            services.AddSingleton(options);

            var rabbitFactory = new ConnectionFactory()
            {
                HostName = options.HostName,
                Port = options.Port,
                VirtualHost = options.VirtualHost,
                UserName = options.UserName,
                Password = options.Password
            };

            var connect = rabbitFactory.CreateConnectionAsync();
            services.AddSingleton(connect);
            services.AddSingleton<ISNRabbitMqClient, SNRabbitMqClient>();
        }
    }
}
