using AutoMapper;
using Email.Services.Processor.Data;
using Email.Services.Processor.Hubs;
using Email.Services.Processor.Models;
using Email.Services.Processor.Models.Dto;
using Microsoft.AspNetCore.SignalR;

namespace Email.Services.Processor.Services
{
    public class LoggerService:ILoggerService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private IMapper _mapper;
        private readonly IHubContext<LogsHub> _hubContext;
        public LoggerService(IServiceScopeFactory serviceScopeFactory,IMapper mapper, IHubContext<LogsHub> hubContext)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        public void AddLogToDatabase(LogsDto logsDto)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    Logs obj = _mapper.Map<Logs>(logsDto);
                    dbContext.Logs.Add(obj);
                    dbContext.SaveChanges();

                    //Notify all connected clients
                    _hubContext.Clients.All.SendAsync("RecieveLog", logsDto);
                }
            }
            catch (Exception ex)
            {
                // Log or handle exception appropriately
                throw;
            }
        }
    }
}
