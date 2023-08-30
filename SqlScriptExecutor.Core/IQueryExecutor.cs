using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlScriptExecutor.Core
{
    public interface IQueryExecutor
    {
        void Run(string quary, string db = "");
    }
}
