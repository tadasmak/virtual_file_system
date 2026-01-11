using SaldoHomework.Domain;
using Xunit;

namespace SaldoHomework.Tests
{
    public class VirtualFileTests
    {
        [Fact]
        public void Constructor_SetsProperties()
        {
            var file = new VirtualFile("document.txt", "/path/to/document.txt");

            Assert.Equal("document.txt", file.Name);
            Assert.Equal("/path/to/document.txt", file.SourcePath);
        }
    }
}