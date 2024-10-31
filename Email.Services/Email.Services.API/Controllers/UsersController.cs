using System.Text.Json;
using AutoMapper;
using Email.Services.API.Data;
using Email.Services.API.Models;
using Email.Services.API.Models.Dto;
using Email.Services.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Email.Services.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly AppDbContext _db;
        private IMapper _mapper;
        private readonly IRabbitMqPublisher _rabbitMqPublisher;
        public UsersController(AppDbContext db, IMapper mapper,IRabbitMqPublisher rabbitMqPublisher)
        {
            _db = db;
            _mapper = mapper;
            _rabbitMqPublisher = rabbitMqPublisher;
        }
        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            try
            {
                IEnumerable<User> usersList = await _db.Users.ToListAsync();
                var users = _mapper.Map<IEnumerable<UserDto>>(usersList);
                return users;

            } catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("sendEmail")]
        public ActionResult<string> SendEmails([FromBody] SendEmailDto sendEmailDto)
        {
            if(sendEmailDto == null || string.IsNullOrEmpty(sendEmailDto.Message) ||sendEmailDto.Users == null || !sendEmailDto.Users.Any())
            {
                return BadRequest("Invalid input data");
            }
            var message = JsonSerializer.Serialize(sendEmailDto);
            _rabbitMqPublisher.PublishMessage(message);
            return Ok("Emails sent successfully");
        }
    }
}
