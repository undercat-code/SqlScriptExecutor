using Serilog;
using Serilog.Sinks.ListOfString;
using SqlScriptExecutor.Core;
using SqlScriptExecutor.DAL.LocalDB;
using SqlScriptExecutor.EmailSender;
using System.Collections.Specialized;
using System.Configuration;


namespace SqlScriptExecutor.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //get app config data
            var appConfig = (NameValueCollection)ConfigurationManager.GetSection("appConfig");
            var emailSenderConfig = (NameValueCollection)ConfigurationManager.GetSection("emailSenderConfig");
            
            //serilog setup
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(appConfig["logFilePath"])
                .WriteTo.Console()
                .CreateLogger();

            var emailConfig = new EmailConfigurator
            {
                email = emailSenderConfig["senderEmailLogin"],
                smtpServer = emailSenderConfig["senderEmailSMTPServer"],
                port = int.Parse(emailSenderConfig["senderEmailPort"]),
                password = emailSenderConfig["senderEmailPassword"]
            };

            var sqlScriptManager = new SqlScriptManager(
                new QueryExecutor(),
                new SendEmail(emailConfig),
                appConfig["scriptFolderPath"],
                appConfig["dbKey"],
                emailSenderConfig["defaultRecipients"]);
                sqlScriptManager.Run();


        }

    }
}