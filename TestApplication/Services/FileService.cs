using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TestApplication.Services
{
    public class FileService : IFileService
    {
        private readonly IConfiguration _configuration;

        public FileService(IConfiguration configuration) => _configuration = configuration;

        public async Task WriteTextInFile((string fileName, string text) value)
        {
            try
            {
                var directory = _configuration["FilePath"];
                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

                var path = Path.Combine(directory, value.fileName);

                await File.WriteAllTextAsync(path, value.text);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}