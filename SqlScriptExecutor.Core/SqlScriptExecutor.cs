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
            //get connection String from config file for DB connection
            var db = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=testDB;Integrated Security=True;Pooling=False";
            SqlConnection connectToSql = new SqlConnection(db);
            connectToSql.Open();
            if (connectToSql.State == ConnectionState.Open)
            {
                //taking file of scripts from collection
                foreach (var item in Collection)
                {
                    //taking script from file of scripts
                    foreach (var sqlScript in item.Scripts)
                    {
                        
                        var command = new SqlCommand(sqlScript, connectToSql);
                        try
                        {
                            command.ExecuteNonQuery();
                            Log.Information($"DONE");
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex, "DB Script Error:");
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentException("Can't connect to DB");
            }
           
        }

    }
}
