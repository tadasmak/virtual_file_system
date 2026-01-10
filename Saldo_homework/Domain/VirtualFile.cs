namespace SaldoHomework.Domain
{
    public class VirtualFile
    {
        public string Name { get; private set; } = "";
        public string SourcePath { get; private set; } = "";

        public VirtualFile() { }

        public VirtualFile(string name, string sourcePath)
        {
            Name = name;
            SourcePath = sourcePath;
        }
    }
}