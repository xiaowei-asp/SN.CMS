using System;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using SN.CMS.Common.RabbitMq;
using Xunit;

namespace SN.CMS.Common.Tests
{
    public class RabbitMqTest
    {
        public RabbitMqTest()
        {
            StartupTest.InitStartup("rabbitmq");
            var client = StartupTest.ServiceProvider.GetRequiredService<ISNRabbitMqClient>();
            client.Subscribe(ReceiveMessage);
        }

        internal void ReceiveMessage(string msg)
        {
            Console.WriteLine(msg);
            Trace.WriteLine(msg);
        }

        [Fact]
        public void Publish()
        {
            var client = StartupTest.ServiceProvider.GetRequiredService<ISNRabbitMqClient>();
            client.Publish("你好");
        }

        [Fact]
        public void Subscrible()
        {
            var client = StartupTest.ServiceProvider.GetRequiredService<ISNRabbitMqClient>();
            client.Subscribe(ReceiveMessage);
        }
    }
}
