using System.Threading.Tasks;

namespace TestApplication.Services
{
    public interface IHttpService
    {
        public Task<T> GetDataFromAPIAsync<T>(string parameters);
    }
}