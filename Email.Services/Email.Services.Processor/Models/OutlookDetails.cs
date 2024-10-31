namespace Email.Services.Processor.Models
{
    public class OutlookDetails
    {
        public string Instance {  get; set; }
        public string Domain { get; set; }
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string EncString { get; set; }
    }
}
