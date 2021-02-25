using Microsoft.Extensions.Configuration;
using System;
using Microsoft.Extensions.DependencyInjection;
using SN.CMS.Common.RabbitMq;

namespace SN.CMS.Common.Tests
{
    public class StartupTest
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static void InitStartup(string rabbitMq)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables();

            var configuration = config.Build();
            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(configuration);
            services.AddRabbitMq();
            var serviceProvider = services.BuildServiceProvider();
            ServiceProvider = serviceProvider;
        }
    }
}
