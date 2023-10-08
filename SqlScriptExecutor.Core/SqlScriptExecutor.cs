using Serilog;
using SqlScriptExecutor.Core.Abstractions;

namespace SqlScriptExecutor.Core
{
    public class SqlScriptExecutor
    {
        public SqlScriptExecutor(List<SqlScript> collection, IQueryExecutor executor)
        {
            Collection = collection;
            Executor = executor;
        }
        public List<SqlScript> Collection { get; set; }
        public IQueryExecutor Executor { get; set; }
        public List<string> LogCollection { get; set; }

        public void ExecuteScripts(string connectionString)
        {
            LogCollection = new List<string>();

            Log.Information($"SQL Script Executer started...");

            //taking file of scripts from collection
            foreach (var item in Collection)
            {
                //taking script from file of scripts
                for (var i = 0; i < item.Scripts.Count; i++)
                {
                    try
                    {
                        Executor.RunExecute(item.Scripts[i], connectionString);
                        Log.Information($"{item.DisplayPath} script #{i + 1}: executed successfully");
                    }
                    catch (Exception ex)
                    {
                        var errorDisplayText = $"Error in {item.DisplayPath} script #{i + 1}:";
                        Log.Error(ex, errorDisplayText);
                        var error = ex.Message;
                        LogCollection.Add(errorDisplayText);
                        LogCollection.Add(error);
                    }
                }
            }
            Log.Information($"SQL Script Executer finished!");
        }
    }
}
