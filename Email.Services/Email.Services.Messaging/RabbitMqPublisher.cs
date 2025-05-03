using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Email.Services.Messaging
{
    public class RabbitMqPublisher : IRabbitMqPublisher
    {
        private readonly IModel _channel;
        private readonly RabbitMqOptions _rabbitMqOptions;
        public RabbitMqPublisher(IOptions<RabbitMqOptions> options)
        {
            _rabbitMqOptions = options.Value;
            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMqOptions.Host,
                Port = _rabbitMqOptions.Port,
                UserName = _rabbitMqOptions.UserName,
                Password = _rabbitMqOptions.Password,
            };
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();

            _channel.ExchangeDeclare(_rabbitMqOptions.Exchange, ExchangeType.Direct,durable:true);
            _channel.QueueDeclare(_rabbitMqOptions.Queue,durable:true,exclusive:false,autoDelete:false,arguments:null);
            _channel.QueueBind(_rabbitMqOptions.Queue, _rabbitMqOptions.Exchange, _rabbitMqOptions.RoutingKey);
        }
        public void PublishMessage(string message)
        {
            var messageBody = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(
                exchange: _rabbitMqOptions.Exchange,
                routingKey: _rabbitMqOptions.RoutingKey,
                basicProperties: null,
                body: messageBody
                );
        }
    }
}
