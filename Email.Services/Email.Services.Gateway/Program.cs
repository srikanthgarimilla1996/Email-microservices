using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:8084").
                WithOrigins("http://localhost:4200").
                WithOrigins("http://172.25.25.60:8084") // Change this to your ip4 domain while hosting
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Required for SignalR with credentials
    });
});
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);
var app = builder.Build();
app.UseCors("CorsPolicy");
app.UseRouting();
app.UseWebSockets();
await app.UseOcelot();
app.Run();
