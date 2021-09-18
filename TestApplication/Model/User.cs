using System.Collections.Generic;

namespace TestApplication.Model
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public IEnumerable<Post> Posts { get; set; }

        public IEnumerable<Todo> Todos { get; set; }
    }
}