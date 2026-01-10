using System.Text.Json.Serialization;

namespace SaldoHomework.Domain
{
    public class VirtualFile
    {
        [JsonInclude] public string Name { get; private set; } = "";
        [JsonInclude] public string SourcePath { get; private set; } = "";

        public VirtualFile() { }

        public VirtualFile(string name, string sourcePath)
        {
            Name = name;
            SourcePath = sourcePath;
        }
    }
}