using SaldoHomework.Domain;
using Xunit;

namespace SaldoHomework.Tests
{
    public class VirtualFileSystemTests
    {
        [Fact]
        public void Constructor_CreatesRootFolder()
        {
            var vfs = new VirtualFileSystem();

            Assert.NotNull(vfs.Root);
            Assert.Equal("/", vfs.Root.Name);
        }

        [Fact]
        public void GetFolder_RootPath_ReturnsRoot()
        {
            var vfs = new VirtualFileSystem();
            var folder = vfs.GetFolder("/");

            Assert.Equal("/", folder.Name);
        }

        [Fact]
        public void GetFolder_ValidPath_ReturnsCorrectFolder()
        {
            var vfs = new VirtualFileSystem();
            vfs.Root.AddFolder("documents");
            vfs.Root.Subfolders["documents"].AddFolder("work");

            var folder = vfs.GetFolder("/documents/work");

            Assert.Equal("work", folder.Name);
        }

        [Fact]
        public void GetFolder_InvalidPath_ThrowsException()
        {
            var vfs = new VirtualFileSystem();

            Assert.Throws<InvalidOperationException>(() => vfs.GetFolder("/nonexistent"));
        }
    }
}