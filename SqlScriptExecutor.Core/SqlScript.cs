using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlScriptExecutor.Core
{
    public class SqlScript
    {
        private string FilePath;
        private string FileContent;
        public SqlScript(string filePath, string fileContent)
        {
            this.FilePath = filePath;
            this.FileContent = fileContent;
        }

        public void Print()
        {
            Console.WriteLine($"{FilePath} \n {FileContent}");
        }
    }
}
