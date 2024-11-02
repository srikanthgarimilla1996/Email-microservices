using Email.Services.Processor.Models.Dto;

namespace Email.Services.Processor.Services
{
    public interface ILoggerService
    {
        void AddLogToDatabase(LogsDto logs);
    }
}
