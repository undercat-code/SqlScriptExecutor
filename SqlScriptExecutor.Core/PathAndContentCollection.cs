using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlScriptExecutor.Core
{
    public class PathAndContentCollection
    {

        public PathAndContentCollection(IEnumerable<string> SearchingResult) => filesPaths = SearchingResult;

        public IEnumerable<string> filesPaths { get; }


        public List<SqlScript> CreatingCollection()
        {
            var collection = new List<SqlScript>();

            foreach (var file in filesPaths)
            {
                var fileContent = File.ReadAllText(file);
                var collectionItem = new SqlScript(file, fileContent);
                collection.Add(collectionItem);
            }
            
            return collection;
        }

    }
}
