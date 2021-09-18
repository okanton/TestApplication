using System.Threading.Tasks;

namespace TestApplication.Services
{
    public interface IFileService
    {
        public Task WriteTextInFile((string fileName, string text) value);
    }
}