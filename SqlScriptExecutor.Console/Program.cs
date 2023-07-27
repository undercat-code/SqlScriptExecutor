using System;
using SqlScriptExecutor.Core;
using System.Data;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Serilog;
using System.Configuration;


namespace SqlScriptExecutor.Console 
{
    class Program
    {
        static void Main(string[] args)
        {
            //connection string for log file
            var log_path_connection = ConfigurationManager.AppSettings.Get("logFilePath");
            var folder_path_connection = ConfigurationManager.AppSettings.Get("scriptFolderPath");



            //serilog setup
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(log_path_connection)
                .WriteTo.Console()
                .CreateLogger();


            //testing and display collection result
            var readingTest = new SqlFileReader(folder_path_connection);
            var scriptsFromFolder = readingTest.GetSqlScripts();

            var launcherTest = new Core.SqlScriptExecutor(scriptsFromFolder);
            launcherTest.ExecuteScripts();

        }
    }
}