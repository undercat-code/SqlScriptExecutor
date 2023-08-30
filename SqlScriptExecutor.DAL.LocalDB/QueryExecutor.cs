using Serilog;
using SqlScriptExecutor.Core;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SqlScriptExecutor.DAL.LocalDB
{
    public class QueryExecutor : IQueryExecutor
    {
               
        public void Run(string quary, string db = "")
        {
            var connectToSql = new SqlConnection(db);
            connectToSql.Open();
            if (connectToSql.State == ConnectionState.Open)
            {
                var command = new SqlCommand(quary, connectToSql);
                command.ExecuteNonQuery();
                
            }
        }
    }
}