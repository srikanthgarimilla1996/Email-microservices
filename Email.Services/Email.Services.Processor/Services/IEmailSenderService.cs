using Email.Services.Processor.Models.Dto;

namespace Email.Services.Processor.Services
{
    public interface IEmailSenderService
    {
        void SendEmail(string message, List<UserDto> UsersList);
    }
}
