using AutoMapper;
using Email.Services.Processor.Models;
using Email.Services.Processor.Models.Dto;

namespace Email.Services.Processor
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<LogsDto, Logs>();
                config.CreateMap<Logs, LogsDto>();
            });
            return mappingConfig;
        }
    }
}
