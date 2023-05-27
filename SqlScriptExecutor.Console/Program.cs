using System;
using SqlScriptExecutor.Core;

namespace SqlScriptExecutor.Console 
{
    class Program
    {
        static void Main(string[] args)
        {
            //testing and display collection result
            var test = new SqlFileReader("D:\\Учёба\\ITVDN");
            var result = test.GetSqlScripts();
            
            foreach (var file in result)
            {
                System.Console.WriteLine($"{file.Path} \n {file.Text}");
            }
        }
    }
}