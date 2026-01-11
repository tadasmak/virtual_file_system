using SaldoHomework.Domain;
using Xunit;

namespace SaldoHomework.Tests
{
    public class VirtualFolderTests
    {
        [Fact]
        public void AddFolder_NewFolder_AddsSuccessfully()
        {
            var folder = new VirtualFolder("root");

            folder.AddFolder("subfolder");

            Assert.True(folder.Subfolders.ContainsKey("subfolder"));
            Assert.Equal("subfolder", folder.Subfolders["subfolder"].Name);
        }

        [Fact]
        public void AddFolder_DuplicateName_ThrowsException()
        {
            var folder = new VirtualFolder("root");
            folder.AddFolder("subfolder");

            Assert.Throws<InvalidOperationException>(() => folder.AddFolder("subfolder"));
        }

        [Fact]
        public void AddFile_NewFile_AddsSuccessfully()
        {
            var folder = new VirtualFolder("root");
            var file = new VirtualFile("test.txt", "/source/test.txt");

            folder.AddFile(file);

            Assert.True(folder.Files.ContainsKey("test.txt"));
            Assert.Equal(file, folder.Files["test.txt"]);
        }

        [Fact]
        public void AddFile_DuplicateName_ThrowsException()
        {
            var folder = new VirtualFolder("root");
            var file1 = new VirtualFile("test.txt", "/source1/test.txt");
            var file2 = new VirtualFile("test.txt", "/source2/test.txt");
            folder.AddFile(file1);

            Assert.Throws<InvalidOperationException>(() => folder.AddFile(file2));
        }
    }
}