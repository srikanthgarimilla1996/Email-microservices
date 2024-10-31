using Email.Services.Processor.Messaging;

namespace Email.Services.Processor.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        //private static IRabbitMqConsumer rabbitMqConsumer {  get; set; }
        public static IApplicationBuilder UseRabbitMqConsumer (this IApplicationBuilder app) 
        {
            var rabbitMqConsumer = app.ApplicationServices.GetService<IRabbitMqConsumer>();
            var hostApplicationLifeTime = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            hostApplicationLifeTime.ApplicationStarted.Register(()=>rabbitMqConsumer.Start());
            hostApplicationLifeTime.ApplicationStopped.Register(() => rabbitMqConsumer.Stop());
            return app;
        }

        //private static void OnStart()
        //{
        //    rabbitMqConsumer.Start();
        //}

        //private static void OnStop()
        //{
        //    rabbitMqConsumer.Stop();
        //}
    }
}
