using System;
using SqlScriptExecutor.Core;

namespace SqlScriptExecutor.Console 
{
    class Program
    {
        static void Main(string[] args)
        {
            //testing and display collection result
            var test = new SqlFilesSearch("D:\\Учёба\\ITVDN");
            var searchResult = test.GetAllSqlFiles();

            var collectionTest = new PathAndContentCollection(searchResult);
            var result = collectionTest.CreatingCollection();

            foreach (var file in result)
            {
                file.Print();
            }
        }
    }
}