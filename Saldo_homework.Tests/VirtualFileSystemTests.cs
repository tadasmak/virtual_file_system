using SaldoHomework.Domain;
using Xunit;

namespace SaldoHomework.Tests
{
    public class VirtualFileSystemTests
    {
        [Fact]
        public void ConstructorCreatesRootFolder()
        {
            var vfs = new VirtualFileSystem();

            Assert.NotNull(vfs.Root);
            Assert.Equal("/", vfs.Root.Name);
        }
    }
}