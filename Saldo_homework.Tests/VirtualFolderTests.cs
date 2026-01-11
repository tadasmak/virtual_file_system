using SaldoHomework.Domain;
using Xunit;

namespace SaldoHomework.Tests
{
    public class VirtualFolderTests
    {
        [Fact]
        public void AddFolder_NewFolderAddsSuccessfully()
        {
            var folder = new VirtualFolder("root");

            folder.AddFolder("subfolder");

            Assert.True(folder.Subfolders.ContainsKey("subfolder"));
            Assert.Equal("subfolder", folder.Subfolders["subfolder"].Name);
        }

        [Fact]
        public void AddFolder_DuplicateNameThrowsException()
        {
            var folder = new VirtualFolder("root");
            folder.AddFolder("subfolder");

            Assert.Throws<InvalidOperationException>(() => folder.AddFolder("subfolder"));
        }
    }
}