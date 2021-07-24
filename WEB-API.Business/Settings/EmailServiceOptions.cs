namespace WEB_API.Business.Settings
{
    public class EmailServiceOptions
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string SmtpUrl { get; set; }
        public int SmtpPort { get; set; }
        public bool UseSsl { get; set; }
    }
}