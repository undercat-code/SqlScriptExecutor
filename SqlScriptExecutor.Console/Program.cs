using EmailService;
using MimeKit;
using SendEmail;
using Serilog;
using Serilog.Sinks.ListOfString;
using SqlScriptExecutor.Core;
using SqlScriptExecutor.DAL.LocalDB;
using System.Configuration;
using System.Text.RegularExpressions;

namespace SqlScriptExecutor.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //connection string for log file
            var logPathConnection = ConfigurationManager.AppSettings.Get("logFilePath");
            var folderPathConnection = ConfigurationManager.AppSettings.Get("scriptFolderPath");
            var dbConnection = ConfigurationManager.AppSettings.Get("dbKey");

            var logCollection = new List<string>();

            //serilog setup
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(logPathConnection)
                .WriteTo.StringList(logCollection, outputTemplate: "{Message} {Exception}")
                .WriteTo.Console()
                .MinimumLevel.Debug()
                .CreateLogger();


            //testing and display collection result
            var readingTest = new SqlFileReader(folderPathConnection);
            var scriptsFromFolder = readingTest.GetSqlScripts();
            var QueryExecutor = new QueryExecutor();

            var launcherTest = new Core.SqlScriptExecutor(scriptsFromFolder, QueryExecutor);
            launcherTest.ExecuteScripts(dbConnection);

            //Check errors in log
            var patternErrorFilter = @"\bError in\b";
            var FilteredErrorCollection = new List<string>();

            foreach (var log in logCollection)
            {
                bool errorFilter = Regex.IsMatch(log, patternErrorFilter);

                if (errorFilter)
                {
                    FilteredErrorCollection.Add(log);
                }
            }
            //Send Email if SQL executor got errors in progress
            if (FilteredErrorCollection.Count > 0)
            {
                //Get email config
                var emailLogin = ConfigurationManager.AppSettings.Get("emailLogin");
                var emailSMTPServer = ConfigurationManager.AppSettings.Get("emailSMTPServer");
                var emailPort = ConfigurationManager.AppSettings.Get("emailPort");
                var emailPassword = ConfigurationManager.AppSettings.Get("emailPassword");
                var emailRecipient = ConfigurationManager.AppSettings.Get("defaultRecipients");


                var emailConfig = new EmailConfigurator
                {
                    Email = emailLogin,
                    SmtpServer = emailSMTPServer,
                    Port = int.Parse(emailPort),
                    Password = emailPassword
                };

                //Reformat Errors collection for Email table
                var LogCollectionReformator = new LogCollectionReformator(FilteredErrorCollection);
                var ErrorCollectionForEmailSendler = LogCollectionReformator.ReformatErrorText();

                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress("email", emailConfig.Email));
                emailMessage.To.AddRange(new List<MailboxAddress> { new MailboxAddress("email", emailRecipient) });
                emailMessage.Subject = "Errors from SQL Executor process:";

                var mailText = $"During the script executing the following errors occured:\r\n<table cellspacing=\"2\" border=\"1\" cellpadding=\"5\" width=\"600\">" + "<tr>\r\n    <th>Script Path</th>\r\n    <th>Error</th>\r\n  </tr>\r\n";
                for (var i = 0; i < ErrorCollectionForEmailSendler.Count; i += 2)
                {
                    mailText += $"<tr>\r\n    <th align=\"left\">{ErrorCollectionForEmailSendler[i]}</th>\r\n    <th align=\"left\">{ErrorCollectionForEmailSendler[i + 1]}</th>\r\n  </tr>\r\n";

                }
                mailText += $"</table>";
                emailMessage.Body = new TextPart("html") { Text = mailText };

                var emailSender = new EmailSender(emailConfig);
                emailSender.Send(emailMessage);
            }

        }

    }
}