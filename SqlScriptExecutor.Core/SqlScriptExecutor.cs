using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            var db = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=newtestdb;Integrated Security=True;Pooling=False";
            SqlConnection connectToSql = new SqlConnection(db);
            connectToSql.Open();
            if (connectToSql.State == ConnectionState.Open)
            {
                foreach (var item in Collection)
                {
                    foreach(var script in item.Scripts)
                    {
                        var command = new SqlCommand(script, connectToSql);
                        command.ExecuteNonQuery();
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
