namespace SaldoHomework.Domain
{
    public class File
    {
        public string Name { get; }
        public string SourcePath { get; }

        public File(string name, string sourcePath)
        {
            Name = name;
            SourcePath = sourcePath;
        }
    }
}