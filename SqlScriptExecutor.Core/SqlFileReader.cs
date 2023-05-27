using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                throw new ArgumentException($"Directory does not exist: {path}");
            }
        }
        public string Path { get; set; }
        
        public IEnumerable<string> searchingFilesResult { get; set; } = null;
    
        public List<SqlScript> GetSqlScripts()
        {
            var collection = new List<SqlScript>();
            searchingFilesResult = Directory.EnumerateFiles(Path, "*.sql", SearchOption.AllDirectories);
                                
            foreach (var file in searchingFilesResult)
            {
                var fileContent = File.ReadAllText(file);
                var collectionItem = new SqlScript(file, fileContent);
                collection.Add(collectionItem);
            }
            
            return collection;
        }
    }
}
