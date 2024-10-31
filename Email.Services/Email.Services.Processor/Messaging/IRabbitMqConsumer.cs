namespace Email.Services.Processor.Messaging
{
    public interface IRabbitMqConsumer
    {
        void Start();
        void Stop();
    }
}
