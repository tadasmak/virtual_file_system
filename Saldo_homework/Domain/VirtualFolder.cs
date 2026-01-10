using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SaldoHomework.Domain
{
    public class VirtualFolder
    {
        [JsonInclude] public string Name { get; private set; } = "";
        [JsonInclude] public Dictionary<string, VirtualFolder> Subfolders { get; private set; } = new();
        [JsonInclude] public Dictionary<string, VirtualFile> Files { get; private set; } = new();

        public VirtualFolder() { } // REQUIRED for JSON

        public VirtualFolder(string name)
        {
            Name = name;
        }

        public void AddFolder(string name)
        {
            if (Subfolders.ContainsKey(name))
            {
                throw new InvalidOperationException("Folder already exists");
            }

            Subfolders[name] = new VirtualFolder(name);
        }

        public void AddFile(VirtualFile file)
        {
            if (Files.ContainsKey(file.Name))
            {
                throw new InvalidOperationException("File already exists");
            }

            Files[file.Name] = file;
        }
    }
}