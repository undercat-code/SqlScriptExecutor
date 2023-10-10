using Serilog;
using SqlScriptExecutor.Core.Abstractions;

namespace SqlScriptExecutor.Core
{
    public class SqlScriptManager
    {
        public IQueryExecutor QueryExecutor { get; set; }
        public IMessageSender MessageSender { get; set; }
        public SqlExecutorConfigurator SqlExecutorConfig { get; set; }
        public string EmailRecipient { get; set; }

        public SqlScriptManager(IQueryExecutor queryExecutor, IMessageSender messageSender, SqlExecutorConfigurator sqlExecutorConfig, string emailRecipient)
        {
            QueryExecutor = queryExecutor;
            MessageSender = messageSender;
            SqlExecutorConfig = sqlExecutorConfig;
            EmailRecipient = emailRecipient;
        }

        public void Run()
        {
            //read sql scripts from folder
            var sqlFileReader = new SqlFileReader(SqlExecutorConfig.ScriptFolderPath);
            var sqlScriptCollection = sqlFileReader.GetSqlScripts();
            //execute sql scripts to DB
            var sqlScriptExecutor = new SqlScriptExecutor(sqlScriptCollection, QueryExecutor);
            sqlScriptExecutor.ExecuteScripts(SqlExecutorConfig.DbKey);
            
            //sending error message to recepients email
            var errorList = sqlScriptExecutor.LogCollection;

            if (errorList.Count > 0)
            {
                var emailMessageBuilder = new EmailMessageBuilder();
                var bodyText = emailMessageBuilder.Build(errorList);
                MessageSender.Send(EmailRecipient, bodyText);
                Log.Information($"Scripts Executor got errors in progress, message was sent to recipient");
            }

        }

    }
}
