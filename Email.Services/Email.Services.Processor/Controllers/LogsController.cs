using AutoMapper;
using Email.Services.Processor.Data;
using Email.Services.Processor.Models;
using Email.Services.Processor.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Email.Services.Processor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly AppDbContext _db;
        private IMapper _mapper;
        public LogsController(AppDbContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<LogsDto>> GetLogs()
        {
            try
            {
                IEnumerable<Logs> LogsList = await _db.Logs.ToListAsync();
                var logs = _mapper.Map<IEnumerable<LogsDto>>(LogsList);
                return logs;

            }catch(Exception ex) 
            {
                throw;
            }
        }
    }
}
