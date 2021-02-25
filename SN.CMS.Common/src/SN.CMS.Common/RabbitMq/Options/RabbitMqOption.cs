
using RabbitMQ.Client;

namespace SN.CMS.Common.RabbitMq
{
    public class RabbitMqOption
    {
        /// <summary>
        /// 主机名
        /// </summary>
        public string HostName { get; set; }

        public int Port { get; set; }

        /// <summary>
        /// 队列名
        /// </summary>
        public string QueueName { get; set; }

        public string VirtualHost { get; set; } = "/";

        public string UserName { get; set; }

        public string Password { get; set; }

        /// <summary>
        /// 是否持久化
        /// </summary>

        public bool Durable { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>

        public bool Exclusive { get; set; } = false;

        /// <summary>
        /// 是否自动删除
        /// </summary>
        public bool AutoDelete { get; set; } = false;

        //public bool Arguments { get; set; }

        /// <summary>
        /// 交换机
        /// </summary>
        public string Exchange { get; set; }

        /// <summary>
        /// 路由键
        /// </summary>

        public string RoutingKey { get; set; }

        public IBasicProperties BasicProperties { get; set; }
    }
}
