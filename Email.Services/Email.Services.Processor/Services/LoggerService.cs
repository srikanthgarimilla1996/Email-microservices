using AutoMapper;
using Email.Services.Processor.Data;
using Email.Services.Processor.Models;
using Email.Services.Processor.Models.Dto;

namespace Email.Services.Processor.Services
{
    public class LoggerService:ILoggerService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private IMapper _mapper;
        public LoggerService(IServiceScopeFactory serviceScopeFactory,IMapper mapper)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
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
