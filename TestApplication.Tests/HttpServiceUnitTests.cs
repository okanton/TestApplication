using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestApplication.Model;
using TestApplication.Services;
using Xunit;

namespace TestApplication.Tests
{
    public class HttpServiceUnitTests
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _configuration;

        public HttpServiceUnitTests()
        {
            var myConfiguration = new Dictionary<string, string>
            {
                {"BaseUrl", "https://jsonplaceholder.typicode.com"},
            };
            _configuration = new ConfigurationBuilder().AddInMemoryCollection(myConfiguration).Build();

            _httpService = new HttpService(_configuration);
        }

        [Fact]
        public async Task GetDataFromAPIAsync_FromRealAPI_ReturnObject()
        {
            //Arrange
            var userId = 3;
            //Act
            var result = await _httpService.GetDataFromAPIAsync<User>($"users/{userId}");
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetDataFromAPIAsync_FromRealAPI_ReturnNull()
        {
            //Arrange
            var userId = 33;
            //Act
            var result = await _httpService.GetDataFromAPIAsync<User>($"users/{userId}");
            //Assert
            Assert.Null(result);
        }
    }
}