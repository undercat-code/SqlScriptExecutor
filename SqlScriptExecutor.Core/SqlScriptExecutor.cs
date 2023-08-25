using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.Metadata.Ecma335;
using Serilog;
using System.Configuration;
using System.Data.Common;


namespace SqlScriptExecutor.Core
{
    public class SqlScriptExecutor
    {
        public SqlScriptExecutor(List<SqlScript> collection)
        {
            this.Collection = collection;
            
        }
        public List<SqlScript> Collection { get; set; }
        
        
        public void ExecuteScripts(IQueryExecutor dbConnection)
        {
            Log.Information($"SQL Script Executer started...");
                //taking file of scripts from collection
            foreach (var item in Collection)
            {
                //taking script from file of scripts
                for(var i = 0; i < item.Scripts.Count; i++)
                {
 
                    
                    try
                    {

                        dbConnection.Run(item.Scripts[i]);
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
