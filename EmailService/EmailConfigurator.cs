using MimeKit;

namespace SendEmail
{
    public class EmailSender
    {
        private readonly EmailConfigurator _config;

        public EmailSender(EmailConfigurator config)
        {
            _config = config;
        }

        public void Send(MimeMessage mailMessage)
        {
            Console.WriteLine("SQL executor got script errors, sending email for recipients...");
            using var client = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                client.Connect(_config.SmtpServer, _config.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_config.Email, _config.Password);
                client.Send(mailMessage);
                Console.WriteLine("\rLetter sent");
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