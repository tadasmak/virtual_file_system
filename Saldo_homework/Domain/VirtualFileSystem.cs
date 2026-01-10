namespace SaldoHomework.Domain
{
    public class VirtualFileSystem
    {
        public VirtualFolder Root { get; }

        public VirtualFileSystem()
        {
            Root = new VirtualFolder("/");
        }
    }
}