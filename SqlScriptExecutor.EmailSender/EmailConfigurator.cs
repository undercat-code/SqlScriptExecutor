using MimeKit;

namespace SqlScriptExecutor.EmailSender
{
    public class EmailConfigurator
    {
        public string email { get; set; }
        public string smtpServer { get; set; }
        public int port { get; set; }
        public string password { get; set; }
    }
    
}