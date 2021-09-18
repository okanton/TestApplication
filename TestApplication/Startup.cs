using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using TestApplication.Services;

namespace TestApplication
{
    public static class Startup
    {
        public static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
            IConfiguration configuration = builder.Build();

            services.AddSingleton(configuration);
            services.AddScoped<EntryPoint>();
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<IDataRepository, DataRepository>();
            services.AddScoped<IFileService, FileService>();

            return services;
        }
    }
}