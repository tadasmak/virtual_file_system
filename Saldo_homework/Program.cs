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
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error: {exception.Message}");
            }
        }
    }
}
