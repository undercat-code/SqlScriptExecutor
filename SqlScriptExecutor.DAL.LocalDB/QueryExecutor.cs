using SqlScriptExecutor.Core.Abstractions;
using System.Data;
using System.Data.SqlClient;

namespace SqlScriptExecutor.DAL.LocalDB
{
    public class QueryExecutor : IQueryExecutor
    {

        public void RunExecute(string query, string connectionString)
        {
            var connectToSql = new SqlConnection(connectionString);
            connectToSql.Open();
            if (connectToSql.State == ConnectionState.Open)
            {
                var command = new SqlCommand(query, connectToSql);
                command.ExecuteNonQuery();
            }
        }
    }
}