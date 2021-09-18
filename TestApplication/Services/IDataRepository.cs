using System.Threading.Tasks;
using TestApplication.Model;

namespace TestApplication.Services
{
    public interface IDataRepository
    {
        public Task<User> GetUserByIdAsync(int userId);
    }
}