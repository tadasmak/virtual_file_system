namespace SaldoHomework.Domain
{
    public class VirtualFileSystem
    {
        public VirtualFolder Root { get; private set; } = null!;

        public VirtualFileSystem()
        {
            Root = new VirtualFolder("/");
        }

        public VirtualFolder GetFolder(string path)
        {
            if (path == "/")
            {
                return Root;
            }

            var parts = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
            var current = Root;

            foreach (var part in parts)
            {
                if (!current.Subfolders.TryGetValue(part, out var next))
                {
                    throw new InvalidOperationException("Invalid path");
                }

                current = next;
            }

            return current;
        }
    }
}