using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlScriptExecutor.Core
{
    public class SqlScript
    {
        public string Path { get; set; }
        public string Text { get; set; }
        public SqlScript(string path, string text)
        {
            this.Path = path;
            this.Text = text;
        }

        
    }
}
