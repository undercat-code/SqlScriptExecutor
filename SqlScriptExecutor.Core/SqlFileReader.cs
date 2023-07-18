using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SqlScriptExecutor.Core
{ 
    public class SqlFileReader
    {
        public SqlFileReader(string path)
        {
            if (Directory.Exists(path))
            {
                this.Path = path;
            }
            else
            {
                Log.Information($"Directory does not exist: {path}");
                throw new ArgumentException($"Directory does not exist: {path}");
            }
        }
        public string Path { get; set; }
        




        public List<SqlScript> GetSqlScripts()
        {
            var searchingFilesResult = Directory.EnumerateFiles(Path, "*.sql", SearchOption.AllDirectories);
            var collection = new List<SqlScript>();
            
            var pattern = "go";
            foreach (var file in searchingFilesResult)
            {
                var fileContent = File.ReadAllText(file);
                var listOfScriptsFromFile = Regex.Split(fileContent, pattern, RegexOptions.IgnoreCase).ToList();
                
                
                //searching empty scripts and delete
                for (int i = listOfScriptsFromFile.Count - 1; i != -1; i--)
                {
                    //pattern - any letter in string
                    bool res = Regex.IsMatch(listOfScriptsFromFile[i], @"[a-zA-Z]+");
                    
                    if (res==false)
                    {
                        listOfScriptsFromFile.RemoveAt(i);
                    }
                       
                }


                //if list is empty it doesn't add in collection
                if (listOfScriptsFromFile.Count != 0)
                {
                    var collectionItem = new SqlScript(file, listOfScriptsFromFile);
                    collection.Add(collectionItem);
                }
                
            }
            
            return collection;
        }
    }
}
