namespace SqlScriptExecutor.Core
{
    public class SqlScript
    {
        public string Path { get; set; }
        public string Text { get; set; }
        public string DisplayPath { get; set; }
        public List<string> Scripts { get; set; }
        public SqlScript(string path, string displayPath, List<string> scripts)
        {
            Path = path;
            DisplayPath = displayPath;
            Scripts = scripts;
        }


    }
}
