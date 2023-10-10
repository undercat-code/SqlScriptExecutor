using MimeKit;
using SqlScriptExecutor.Core.Abstractions;

namespace SqlScriptExecutor.EmailSender
{
    public class SendEmail : IMessageSender
    {
        private readonly EmailConfigurator Config;

        public SendEmail(EmailConfigurator config)
        {
            Config = config;
        }

        public void Send(string to, string body, string subject = "")
        {
            using var client = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress("email", Config.Email));
                emailMessage.To.AddRange(new List<MailboxAddress> { new MailboxAddress("email", to) });
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart("html") { Text = body };
                client.Connect(Config.SmtpServer, Config.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(Config.Email, Config.Password);
                client.Send(emailMessage);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }


}