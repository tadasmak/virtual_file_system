using System;
using SaldoHomework.Domain;

class Program
{
    static void Main(string[] args)
    {
        var vfs = new VirtualFileSystem();
        Console.WriteLine("Virtual File System CLI.");

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

            try
            {
                switch (command)
                {
                    case "help":
                        Console.WriteLine("Commands: createdir <path>, list <path>, tree, exit");
                        break;

                    case "exit":
                        return;

                    case "createdir":
                        var dirPath = argument;
                        HandleMakeDirectory(vfs, dirPath);
                        break;

                    case "addfile":
                        var argsSplit = argument.Split(' ', 2);
                        if (argsSplit.Length != 2)
                        {
                            Console.WriteLine("Usage: addfile <sourcePath> <virtualFolderPath>");
                            break;
                        }

                        var sourcePath = argsSplit[0];
                        var folderPath = argsSplit[1];
                        HandleAddFile(vfs, sourcePath, folderPath);
                        break;

                    case "list":
                        var listPath = string.IsNullOrEmpty(argument) ? "/" : argument;
                        HandleList(vfs, listPath);
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
        Console.WriteLine($"Adding: /{folderPath}/{sourcePath}");
    }
}
