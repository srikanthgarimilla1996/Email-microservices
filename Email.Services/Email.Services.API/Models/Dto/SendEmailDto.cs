namespace Email.Services.API.Models.Dto
{
    public class SendEmailDto
    {
        public string Message { get; set; }
        public List<UserDto> Users { get; set; }
    }
}
