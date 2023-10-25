using SqlScriptExecutor.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlScriptExecutor.Core
{
    public class SqlScriptExecutionConfigurator : ISqlScriptExecutionConfigurator
    {
        public string LogFilePath { get; set; }
        public string ScriptFolderPath { get; set; }
        public string ConnectionString { get; set; }
        public string DefaultRecipients { get; set; }
    }
}
