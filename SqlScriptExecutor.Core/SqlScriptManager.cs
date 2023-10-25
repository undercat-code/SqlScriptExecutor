using Serilog;
using SqlScriptExecutor.Core.Abstractions;

namespace SqlScriptExecutor.Core
{
    public class SqlScriptManager
    {
        public IQueryExecutor QueryExecutor { get; set; }
        public IMessageSender MessageSender { get; set; }
        public ISqlScriptExecutionConfigurator SqlScriptExecutionConfigurator { get; set; }

        public SqlScriptManager(
          IQueryExecutor queryExecutor,
          IMessageSender messageSender,
          ISqlScriptExecutionConfigurator sqlScriptExecutionConfigurator)
        {
            QueryExecutor = queryExecutor;
            MessageSender = messageSender;
            SqlScriptExecutionConfigurator = sqlScriptExecutionConfigurator;
        }

        public void Run()
        {
            //read sql scripts from folder
            var sqlFileReader = new SqlFileReader(SqlScriptExecutionConfigurator.ScriptFolderPath);
            var sqlScriptCollection = sqlFileReader.GetSqlScripts();
            //execute sql scripts to DB
            var sqlScriptExecutor = new SqlScriptExecutor(sqlScriptCollection, QueryExecutor);
            sqlScriptExecutor.ExecuteScripts(SqlScriptExecutionConfigurator.ConnectionString);
            
            //sending error message to recepients email
            var errorList = sqlScriptExecutor.LogCollection;

            if (errorList.Count > 0)
            {
                var emailMessageBuilder = new EmailMessageBuilder();
                var bodyText = emailMessageBuilder.Build(errorList);
                MessageSender.Send(SqlScriptExecutionConfigurator.DefaultRecipients, bodyText);
                Log.Information($"Scripts Executor got errors in progress, message was sent to recipient");
            }

        }

    }
}
