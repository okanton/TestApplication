using System.Text;
using TestApplication.Model;

namespace TestApplication.Extentions
{
    public static class StringExtentions
    {
        public static (string filePath, string fileText) GetFilePathText(this User user)
        {
            var filePath = $"{user.Id}-{user.UserName}.txt";

            var builder = new StringBuilder();
            builder.AppendLine($"Уважаемый {user.UserName},\nниже представлен список ваших  действий за последнее время.\nВыполнено задач:");

            int i = 1;
            foreach (var todo in user.Todos)
            {
                builder.AppendLine($"{i}. {todo.Title}");
                i++;
            }

            builder.AppendLine($"Написано постов:");

            i = 1;
            foreach (var post in user.Posts)
            {
                builder.AppendLine($"{i}. {post.Title}");
                i++;
            }

            return (filePath, builder.ToString());
        }
    }
}