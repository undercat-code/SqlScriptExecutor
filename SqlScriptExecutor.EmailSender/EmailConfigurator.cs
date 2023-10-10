using MimeKit;

namespace SqlScriptExecutor.EmailSender
{
    public class EmailConfigurator
    {
        public string Email { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
    }
}