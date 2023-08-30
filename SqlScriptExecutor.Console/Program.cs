using System;
using SqlScriptExecutor.Core;
using System.Data;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Serilog;
using System.Configuration;
using SqlScriptExecutor.DAL.LocalDB;




namespace SqlScriptExecutor.Console 
{
    class Program
    {
        static void Main(string[] args)
        {
            //connection string for log file
            var logPathConnection = ConfigurationManager.AppSettings.Get("logFilePath");
            var folderPathConnection = ConfigurationManager.AppSettings.Get("scriptFolderPath");
            var dbConnection = ConfigurationManager.AppSettings.Get("dbKey");

            //serilog setup
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(logPathConnection)
                .WriteTo.Console()
                .CreateLogger();


            //testing and display collection result
            var readingTest = new SqlFileReader(folderPathConnection);
            var scriptsFromFolder = readingTest.GetSqlScripts();
            var QueryExecutor = new QueryExecutor();

            var launcherTest = new Core.SqlScriptExecutor(scriptsFromFolder, QueryExecutor);
            launcherTest.ExecuteScripts(dbConnection);

        }
    }
}