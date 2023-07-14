using System;
using SqlScriptExecutor.Core;
using System.Data;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Serilog;


namespace SqlScriptExecutor.Console 
{
    class Program
    {
        static void Main(string[] args)
        { //serilog setup
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("D:\\Учёба\\sql\\log.txt")
                .CreateLogger();


            //testing and display collection result
            var readingTest = new SqlFileReader("D:\\Учёба\\sql");
            var scriptsFromFolder = readingTest.GetSqlScripts();

            foreach (var SqlScripts in scriptsFromFolder)
            {
                var checking = SqlScripts.Scripts;
                foreach (var script in checking)
                {
                    System.Console.WriteLine(script);
                    System.Console.WriteLine("---------------------------------------------------------");
                }
            }

            var launcherTest = new Core.SqlScriptExecutor(scriptsFromFolder);
            launcherTest.GetAndUseScript();

        }
    }
}