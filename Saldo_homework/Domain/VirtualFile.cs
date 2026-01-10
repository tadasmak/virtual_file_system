namespace SaldoHomework.Domain
{
    public class VirtualFile
    {
        public string Name { get; }
        public string SourcePath { get; }

        public VirtualFile(string name, string sourcePath)
        {
            Name = name;
            SourcePath = sourcePath;
        }
    }
}