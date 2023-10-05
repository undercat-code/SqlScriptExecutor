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
            this.Path = path;
            this.DisplayPath = displayPath;
            this.Scripts = scripts;
        }


    }
}
