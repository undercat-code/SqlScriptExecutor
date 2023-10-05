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
            //connection string for log file
            NameValueCollection appConfigConnection = (NameValueCollection)ConfigurationManager.GetSection("appConfig");
            var logPathConnection = appConfigConnection["logFilePath"];
            var folderPathConnection = appConfigConnection["scriptFolderPath"];
            var dbConnection = appConfigConnection["dbKey"];

            var logCollection = new List<string>();

            //serilog setup
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(logPathConnection)
                .WriteTo.StringList(logCollection, outputTemplate: "{Message} {Exception}")
                .WriteTo.Console()
                .CreateLogger();


            //testing and display collection result
            var readingFolder = new SqlFileReader(folderPathConnection);
            var scriptsFromFolder = readingFolder.GetSqlScripts();
            var queryExecutor = new QueryExecutor();


            //execute scripts to DB
            var sqlScriptExecute = new Core.SqlScriptExecutor(scriptsFromFolder, queryExecutor);
            sqlScriptExecute.ExecuteScripts(dbConnection);

            //Get email config
            NameValueCollection emailSenderConfigConnection = (NameValueCollection)ConfigurationManager.GetSection("emailSenderConfig");
            var senderEmailLogin = emailSenderConfigConnection["senderEmailLogin"];
            var senderEmailSMTPServer = emailSenderConfigConnection["senderEmailSMTPServer"];
            var senderEmailPort = emailSenderConfigConnection["senderEmailPort"];
            var senderEmailPassword = emailSenderConfigConnection["senderEmailPassword"];
            var emailRecipient = emailSenderConfigConnection["defaultRecipients"];

            var emailConfig = new EmailConfigurator
            {
                email = senderEmailLogin,
                smtpServer = senderEmailSMTPServer,
                port = int.Parse(senderEmailPort),
                password = senderEmailPassword
            };
            //Message sender 
            var logCollectionReformator = new LogCollectionReformator();
            var messageText = new EmailMessageBuilder();
            var emailSender = new SendEmail(emailConfig);
            var sqlScriptManager = new SqlScriptManager(logCollection, logCollectionReformator, messageText, emailSender, emailRecipient);
            sqlScriptManager.SendMessage();


        }

    }
}