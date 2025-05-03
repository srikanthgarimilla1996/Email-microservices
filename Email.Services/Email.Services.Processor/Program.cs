using AutoMapper;
using Email.Services.Messaging;
using Email.Services.Processor.Data;
using Email.Services.Processor.Extensions;
using Email.Services.Processor.Messaging;
using Email.Services.Processor.Models;
using Email.Services.Processor.Services;
using Microsoft.EntityFrameworkCore;
using Email.Services.Processor;
using Email.Services.Processor.Hubs;

var builder = WebApplication.CreateBuilder(args);
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name:myAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins("http://localhost:8084")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Required for SignalR with credentials
    });
});
System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Add services to the container.
builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMQ"));
builder.Services.Configure<OutlookDetails>(builder.Configuration.GetSection("OutlookCredentials"));
builder.Services.AddSingleton<IRabbitMqConsumer, RabbitMqConsumer>();
builder.Services.AddSingleton<IEmailSenderService, EmailSenderService>();
builder.Services.AddSingleton<ILoggerService, LoggerService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(myAllowSpecificOrigins);
app.MapControllers();
ApplyMigration();
app.UseRabbitMqConsumer();
app.MapHub<LogsHub>("/logsHub");
app.Run();

void ApplyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }
    }
}
