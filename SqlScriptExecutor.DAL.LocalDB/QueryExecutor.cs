using Serilog;
using SqlScriptExecutor.Core;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SqlScriptExecutor.DAL.LocalDB
{
    public class QueryExecutor : IQueryExecutor
    {
               
        public void Run(string quarry, string db = "")
        {
            var db_connection = ConfigurationManager.AppSettings.Get("dbKey");
            var connectToSql = new SqlConnection(db_connection);
            connectToSql.Open();
            if (connectToSql.State == ConnectionState.Open)
            {
                var command = new SqlCommand(quarry, connectToSql);
                command.ExecuteNonQuery();
                
            }
        }
    }
}