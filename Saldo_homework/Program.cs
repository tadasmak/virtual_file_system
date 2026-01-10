using System;
using System.Text.Json;
using SaldoHomework.Domain;

class Program
{
    const string SaveFile = "vfs.json";

    static void Main(string[] args)
    {
        var vfs = LoadVFS();

        Console.WriteLine("Virtual File System CLI. Type 'help' for commands.");

        while (true)
        {
            Console.Write("> ");
            var input = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(input))
            {
                continue;
            }

            var parts = input.Split(' ', 2);
            var command = parts[0].ToLower();
            var argument = parts.Length > 1 ? parts[1] : "";

            string? sourcePath = null;
            string? folderPath = null;

            if (command == "addfile" || command == "removefile")
            {
                var result = SplitSourceAndFolderPaths(argument, command);
                if (result == null) continue;

                sourcePath = result.Value.sourcePath;
                folderPath = result.Value.folderPath;
            }

            try
            {
                switch (command)
                {
                    case "help":
                        Console.WriteLine("Commands: createdir <path>, list <path>, tree, exit");
                        break;

                    case "exit":
                        SaveVFS(vfs);
                        return;

                    case "createdir":
                        HandleMakeDirectory(vfs, argument);
                        break;

                    case "removedir":
                        HandleRemoveDirectory(vfs, argument);
                        break;

                    case "addfile":
                        HandleAddFile(vfs, sourcePath!, folderPath!);
                        break;

                    case "removefile":
                        HandleRemoveFile(vfs, sourcePath!, folderPath!);
                        break;

                    case "list":
                        HandleList(vfs, argument);
                        break;

                    case "tree":
                        PrintTree(vfs.Root, "");
                        break;

                    default:
                        Console.WriteLine("Unknown command. Type 'help' for help.");
                        break;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error: {exception.Message}");
            }
        }
    }

    static void HandleMakeDirectory(VirtualFileSystem vfs, string path)
    {
        var parts = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0)
        {
            Console.WriteLine("Invalid path");
            return;
        }

        var current = vfs.Root;
        for (int i = 0; i < parts.Length; i++)
        {
            var part = parts[i];
            if (!current.Subfolders.ContainsKey(part))
            {
                current.AddFolder(part);
            }
            current = current.Subfolders[part];
        }

        Console.WriteLine($"Folder '{path}' created.");
    }

    static void HandleRemoveDirectory(VirtualFileSystem vfs, string path)
    {
        if (string.IsNullOrWhiteSpace(path) || path == "/")
        {
            Console.WriteLine("Cannot remove root or empty path.");
            return;
        }

        var parts = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        var folderName = parts[^1];
        var parentPath = string.Join("/", parts, 0, parts.Length - 1);
        var parent = string.IsNullOrEmpty(parentPath) ? vfs.Root : vfs.GetFolder("/" + parentPath);

        if (!parent.Subfolders.ContainsKey(folderName))
        {
            Console.WriteLine($"Folder '{folderName}' does not exist.");
            return;
        }

        parent.Subfolders.Remove(folderName);
        Console.WriteLine($"Folder '{path}' removed.");
    }

    static void HandleList(VirtualFileSystem vfs, string path)
    {
        var folder = vfs.GetFolder(path);
        Console.WriteLine("Folders:");
        foreach(var sub in folder.Subfolders.Keys)
        {
            Console.WriteLine($" {sub}");
        }

        Console.WriteLine("Files:");
        foreach (var file in folder.Files.Keys)
        {
            Console.WriteLine($"  {file}");
        }
    }

    static void PrintTree(VirtualFolder folder, string indent)
    {
        Console.WriteLine($"{indent}{folder.Name}/");

        foreach (var sub in folder.Subfolders.Values)
        {
            PrintTree(sub, indent + "  ");
        }

        foreach (var file in folder.Files.Values)
        {
            Console.WriteLine($"{indent}  {file.Name}");
        }
    }

    static void HandleAddFile(VirtualFileSystem vfs, string sourcePath, string folderPath)
    {
        if (!File.Exists(sourcePath))
        {
            Console.WriteLine($"Source file '{sourcePath}' does not exist.");
            return;
        }

        var folder = vfs.GetFolder(folderPath);
        var fileName = Path.GetFileName(sourcePath);
        var file = new VirtualFile(fileName, sourcePath);

        try
        {
            folder.AddFile(file);
            Console.WriteLine($"File '{fileName}' added to '{folderPath}'.");
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine($"File '{fileName}' already exists in '{folderPath}'.");
        }
    }

    static void HandleRemoveFile(VirtualFileSystem vfs, string sourcePath, string folderPath)
    {
        var folder = vfs.GetFolder(folderPath);

        if (!folder.Files.ContainsKey(sourcePath))
        {
            Console.WriteLine($"File '{sourcePath}' does not exist in '{folderPath}'.");
            return;
        }

        folder.Files.Remove(sourcePath);
        Console.WriteLine($"File '{sourcePath}' removed from '{folderPath}'.");
    }

    static (string sourcePath, string folderPath)? SplitSourceAndFolderPaths(string argument, string command)
    {
        var argsSplit = argument.Split(' ', 2);
        if (argsSplit.Length != 2)
        {
            Console.WriteLine($"Usage: {command} <sourcePath> <virtualFolderPath>");
            return null;
        }

        var sourcePath = argsSplit[0];
        var folderPath = argsSplit[1];

        return (sourcePath, folderPath);
    }

    static VirtualFileSystem LoadVFS()
    {
        if (!File.Exists(SaveFile))
        {
            return new VirtualFileSystem();
        }
        
        try
        {
            var json = File.ReadAllText(SaveFile);
            return JsonSerializer.Deserialize<VirtualFileSystem>(json) ?? new VirtualFileSystem();
        }
        catch
        {
            Console.WriteLine("Warning: Failed to load saved VFS. Starting fresh.");
            return new VirtualFileSystem();
        }
    }

    static void SaveVFS(VirtualFileSystem vfs)
    {
        var json = JsonSerializer.Serialize(vfs, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(SaveFile, json);
    }
}
