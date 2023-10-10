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

            var sqlExecutorConfig = new SqlExecutorConfigurator
            {
                ScriptFolderPath = appConfig["scriptFolderPath"],
                DbKey = appConfig["dbKey"]
            };

            var emailConfig = new EmailConfigurator
            {
                Email = emailSenderConfig["senderEmailLogin"],
                SmtpServer = emailSenderConfig["senderEmailSMTPServer"],
                Port = int.Parse(emailSenderConfig["senderEmailPort"]),
                Password = emailSenderConfig["senderEmailPassword"]
            };

            var sqlScriptManager = new SqlScriptManager(
                new QueryExecutor(),
                new SendEmail(emailConfig),
                sqlExecutorConfig,
                emailSenderConfig["defaultRecipients"]);
                sqlScriptManager.Run();


        }

    }
}