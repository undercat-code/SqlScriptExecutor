namespace SqlScriptExecutor.Core.Abstractions
{
    public interface IQueryExecutor
    {
        void RunExecute(string query, string db = "");
    }
}
