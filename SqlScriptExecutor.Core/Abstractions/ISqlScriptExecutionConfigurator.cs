using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlScriptExecutor.Core.Abstractions
{
    public interface ISqlScriptExecutionConfigurator
    {
        string LogFilePath { get; set; }
        string ScriptFolderPath { get; set; }
        string ConnectionString { get; set; }
        string DefaultRecipients { get; set; }
    }
}
