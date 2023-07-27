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
        
        public void ExecuteScripts()
        {
            //connection String from config file for DB connection
            var db_connection = ConfigurationManager.AppSettings.Get("dbKey");

            SqlConnection connectToSql = new SqlConnection(db_connection);
            connectToSql.Open();
            if (connectToSql.State == ConnectionState.Open)
            {
                Log.Information($"SQL Script Executer started...");
                //taking file of scripts from collection
                foreach (var item in Collection)
                {
                   //taking script from file of scripts
                    for(var i = 0; i < item.Scripts.Count; i++)
                    {
 
                        var command = new SqlCommand(item.Scripts[i], connectToSql);
                        try
                        {
                            
                            command.ExecuteNonQuery();
                            Log.Information($"{item.DisplayPath} script #{i+1}: executed successfully");

                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex, $"Error in {item.DisplayPath} script #{i+1}:");
                        }
                    }
                        
                }
                Log.Information($"SQL Script Executer finished!");
            }
            else
            {
                Log.Information($"Can't connect to DB");
                throw new ArgumentException("Check if the DB exists");
            }
           
        }

    }
}
