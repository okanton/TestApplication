using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace TestApplication
{
    public class Program
    {
        public async static Task Main()
        {
            var services = Startup.ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            await serviceProvider.GetService<EntryPoint>().Run();
        }
    }
}