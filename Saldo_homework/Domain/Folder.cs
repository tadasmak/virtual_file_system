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
}