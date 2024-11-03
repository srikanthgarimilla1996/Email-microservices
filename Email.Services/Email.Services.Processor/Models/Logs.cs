using System.ComponentModel.DataAnnotations;

namespace Email.Services.Processor.Models
{
    public class Logs
    {
        [Key]
        public int Id { get; set; }
        public string message {  get; set; }
        public string users { get; set; }
        public string LogLevel { get; set; }
        public string exception { get; set; }
        public DateTime DateTime { get; set; }
    }
}
