public class VirtualFolder
{
    public string Name { get; }
    public Dictionary<string, VirtualFolder> Subfolders { get; }
    public Dictionary<string, VirtualFile> Files { get; }

    public VirtualFolder(string name)
    {
        Name = name;
        Subfolders = new Dictionary<string, VirtualFolder>();
        Files = new Dictionary<string, VirtualFile>();
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

        Files[file.name] = file;
    }
}