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
                        Console.WriteLine("Commands: mkdir <path>, ls <path>, tree, exit");
                        break;

                    case "exit":
                        return;

                    case "createdir":
                        var folderPath = argument;
                        HandleMakeDirectory(vfs, folderPath);
                        break;

                    case "list":
                        var listPath = string.IsNullOrEmpty(argument) ? "/" : argument;
                        HandleList(vfs, listPath);
                        break;

                    case "tree":
                        PrintTree(vfs.Root);
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

    }

    static void HandleList(VirtualFileSystem vfs, string path)
    {

    }

    static void PrintTree(VirtualFolder folder)
    {

    }
}
