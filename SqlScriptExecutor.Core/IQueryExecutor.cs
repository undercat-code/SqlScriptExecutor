namespace SqlScriptExecutor.Core
{
    public interface IQueryExecutor
    {
        void Run(string query, string db = "");
    }
}
