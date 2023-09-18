﻿using Serilog;


namespace SqlScriptExecutor.Core
{
    public class SqlScriptExecutor
    {
        public SqlScriptExecutor(List<SqlScript> collection, IQueryExecutor executor)
        {
            this.Collection = collection;
            this.Executor = executor;

        }
        public List<SqlScript> Collection { get; set; }
        public IQueryExecutor Executor { get; set; }



        public void ExecuteScripts(string connectionString)
        {
            Log.Information($"SQL Script Executer started...");
            //taking file of scripts from collection
            foreach (var item in Collection)
            {
                //taking script from file of scripts
                for (var i = 0; i < item.Scripts.Count; i++)
                {


                    try
                    {

                        Executor.Run(item.Scripts[i], connectionString);
                        Log.Information($"{item.DisplayPath} script #{i + 1}: executed successfully");
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, $"Error in {item.DisplayPath} script #{i + 1}:");
                    }

                }

            }
            Log.Information($"SQL Script Executer finished!");



        }

    }
}
