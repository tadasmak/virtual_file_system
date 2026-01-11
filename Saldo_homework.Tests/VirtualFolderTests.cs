using SaldoHomework.Domain;
using Xunit;

namespace SaldoHomework.Tests
{
    public class VirtualFolderTests
    {
        [Fact]
        public void AddFolderNewFolderAddsSuccessfully()
        {
            var folder = new VirtualFolder("root");

            folder.AddFolder("subfolder");

            Assert.True(folder.Subfolders.ContainsKey("subfolder"));
            Assert.Equal("subfolder", folder.Subfolders["subfolder"].Name);
        }
    }
}