using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApplication.Model;

namespace TestApplication.Services
{
    public class DataRepository : IDataRepository
    {
        private readonly IHttpService _httpService;

        public DataRepository(IHttpService httpService) => _httpService = httpService;

        public async Task<User> GetUserByIdAsync(int userId)
        {
            try
            {
                var userTask = _httpService.GetDataFromAPIAsync<User>($"users/{userId}");

                var todosTask = _httpService.GetDataFromAPIAsync<IEnumerable<Todo>>($"todos?userId={userId}");

                var postsTask = _httpService.GetDataFromAPIAsync<IEnumerable<Post>>($"posts?userId={userId}");

                var user = await userTask;

                if (user is not null)
                {
                    await Task.WhenAll(postsTask, todosTask);

                    var posts = postsTask.Result.OrderByDescending(p => p.Id).Take(5).ToList();
                    var todos = todosTask.Result.Where(p => p.Completed).ToList();

                    user.Posts = posts;
                    user.Todos = todos;

                    await Task.Delay(3000); // Иммитация загрузки

                    return user;
                }
                else throw new Exception("Пользователя с таким ID не существует");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}