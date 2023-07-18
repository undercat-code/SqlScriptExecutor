﻿using System;
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


namespace SqlScriptExecutor.Core
{
    public class SqlScriptExecutor
    {
        public SqlScriptExecutor(List<SqlScript> collection)
        {
            this.Collection = collection;
        }
        public List<SqlScript> Collection { get; set; }
        
        public void GetAndUseScript()
        {
            //connection String from config file for DB connection
            var db_connection = ConfigurationManager.ConnectionStrings["db_key"].ConnectionString;
            
            SqlConnection connectToSql = new SqlConnection(db_connection);
            connectToSql.Open();
            if (connectToSql.State == ConnectionState.Open)
            {
                Log.Information($"SQL Script Executer started...");
                //taking file of scripts from collection
                foreach (var item in Collection)
                {
                    var nomberOfScript = 1;
                    
                    //taking script from file of scripts
                    foreach (var sqlScript in item.Scripts)
                    {
                        
                        var directory = new DirectoryInfo(item.Path);
                        
                        var command = new SqlCommand(sqlScript, connectToSql);
                        try
                        {
                            
                            command.ExecuteNonQuery();
                                Log.Information($"{directory.Parent}\\{directory.Name} script #{nomberOfScript}: executed successfully");

                            }
                        catch (Exception ex)
                        {
                            Log.Error(ex, $"Error in {directory.Parent}\\{directory.Name} script #{nomberOfScript}:");
                        }
                        
                        nomberOfScript++;
                    }
                }
                Log.Information($"SQL Script Executer finished!");
            }
            else
            {
                throw new ArgumentException("Can't connect to DB");
            }
           
        }

    }
}
