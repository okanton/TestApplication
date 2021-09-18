using AutoFixture;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TestApplication.Extentions;
using TestApplication.Model;
using TestApplication.Services;
using Xunit;

namespace TestApplication.Tests
{
    public class FileServiceUnitTests
    {
        private readonly IFileService _fileService;
        private readonly IConfiguration _configuration;

        public FileServiceUnitTests()
        {
            var myConfiguration = new Dictionary<string, string>
            {
                {"FilePath", "C:\\TestApplicationFile"}
            };
            _configuration = new ConfigurationBuilder().AddInMemoryCollection(myConfiguration).Build();
            _fileService = new FileService(_configuration);
        }

        [Fact]
        public async Task WriteTextInFile_WithFakeUser_CheckExistFile()
        {
            // Arrage
            var fixture = new Fixture();
            var mockUser = fixture.Build<User>().With(p => p.Id, 2).Create();
            var filePathText = mockUser.GetFilePathText();
            var fileName = Path.GetFileName(filePathText.filePath);
            // Act
            await _fileService.WriteTextInFile(filePathText);

            // Assert
            Assert.True(File.Exists(Path.Combine(_configuration["FilePath"], filePathText.filePath)));
            Assert.Equal(filePathText.filePath, Path.GetFileName(filePathText.filePath));
        }
    }
}