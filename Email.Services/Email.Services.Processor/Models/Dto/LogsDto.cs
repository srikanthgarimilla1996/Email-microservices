namespace Email.Services.Processor.Models.Dto
{
    public class LogsDto
    {
        public int Id { get; set; }
        public string message { get; set; }
        public string users { get; set; }
        public string LogLevel { get; set; }
        public string exception { get; set; }
        public DateTime DateTime { get; set; }
    }
}
