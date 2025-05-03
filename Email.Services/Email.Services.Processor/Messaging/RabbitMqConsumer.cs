using System.Text;
using System.Text.Json;
using Email.Services.Messaging;
using Email.Services.Processor.Models.Dto;
using Email.Services.Processor.Services;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Email.Services.Processor.Messaging
{
    public class RabbitMqConsumer : IRabbitMqConsumer
    {
        private readonly IModel _channel;
        private readonly RabbitMqOptions _rabbitMqOptions;
        private EventingBasicConsumer _consumer;
        private readonly IEmailSenderService _emailSenderService;

        public RabbitMqConsumer(IOptions<RabbitMqOptions> options,IEmailSenderService emailSenderService)
        {
            _rabbitMqOptions = options.Value;
            _emailSenderService = emailSenderService;
            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMqOptions.Host,
                Port = _rabbitMqOptions.Port,
                UserName = _rabbitMqOptions.UserName,
                Password = _rabbitMqOptions.Password,
            };
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();

            _channel.QueueDeclare(_rabbitMqOptions.Queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }
        public void Start()
        {
            _consumer = new EventingBasicConsumer(_channel);
            _consumer.Received += onMessageReceived;

            _channel.BasicConsume(queue:_rabbitMqOptions.Queue,autoAck:true,consumer:_consumer);
            Console.WriteLine("RabbitMQ consumer started.");
        }

        public void onMessageReceived(object model, BasicDeliverEventArgs ea)
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            ProcessMessage(message);
        }

        private void ProcessMessage(string message)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var data = JsonSerializer.Deserialize<SendEmailDto>(message,options);
            Console.WriteLine(data);
            _emailSenderService.SendEmail(data.Message,data.Users);
        }
        public void Stop()
        {
            _channel.Close();
            Console.WriteLine("RabbitMQ consumer stopped.");
        }
    }
}
