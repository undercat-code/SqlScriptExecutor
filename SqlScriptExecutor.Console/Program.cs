using Serilog;
using SqlScriptExecutor.Core;
using SqlScriptExecutor.DAL.LocalDB;
using SqlScriptExecutor.EmailSender;
using System.Collections.Specialized;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SqlScriptExecutor.Core.Abstractions;

namespace SqlScriptExecutor.Console
{
    class Program
    {
        public static IServiceProvider? ServiceProvider { get; private set; }

        static void Main(string[] args)
        {
            // Create configs
            var appConfig = (NameValueCollection)ConfigurationManager.GetSection("appConfig");
            ISqlScriptExecutionConfigurator sqlScriptExecutionConfigurator = new SqlScriptExecutionConfigurator
            {
                LogFilePath = appConfig["logFilePath"],
                ScriptFolderPath = appConfig["scriptFolderPath"],
                ConnectionString = appConfig["dbKey"],
                DefaultRecipients = appConfig["defaultRecipients"]
            };

            var emailSenderConfig = (NameValueCollection)ConfigurationManager.GetSection("emailSenderConfig");
            var emailConfigurator = new EmailConfigurator
            {
                Email = emailSenderConfig["senderEmailLogin"],
                SmtpServer = emailSenderConfig["senderEmailSMTPServer"],
                Port = int.Parse(emailSenderConfig["senderEmailPort"]),
                Password = emailSenderConfig["senderEmailPassword"]
            };

            // Serilog setup
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(sqlScriptExecutionConfigurator.LogFilePath)
                .WriteTo.Console()
                .CreateLogger();

            // Create Service provider
            ServiceProvider = Host.CreateDefaultBuilder()
                .ConfigureServices(services => {
                    services.AddSingleton(sqlScriptExecutionConfigurator);
                    services.AddSingleton(emailConfigurator);
                    services.AddTransient<IMessageSender, SendEmail>();
                    services.AddTransient<IQueryExecutor, QueryExecutor>();
                    services.AddSingleton<SqlScriptManager>();
                })
                .Build()
                .Services;

            // Create instance of SqlScriptManager
            var sqlScriptManager = ServiceProvider.GetRequiredService<SqlScriptManager>();
            sqlScriptManager.Run();
        }
    }
}