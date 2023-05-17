namespace SqlScriptExecutor.Core
{
    public class SqlFilesSearch
    {
        public SqlFilesSearch(string path) => Path = path;

        public string Path { get; set; }


        //Return all sql files from main directory and subdirectories
        public IEnumerable<string> GetAllSqlFiles()

        {
            IEnumerable<string> searchingFilesResult = null;

            try
            {
                searchingFilesResult = Directory.EnumerateFiles(Path, "*.sql", SearchOption.AllDirectories);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return searchingFilesResult;
        }
    }
}