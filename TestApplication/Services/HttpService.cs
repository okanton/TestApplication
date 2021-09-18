using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TestApplication.Services
{
    public class HttpService : IHttpService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient httpClient;

        public HttpService(IConfiguration configuration)
        {
            _configuration = configuration;

            var handler = new HttpClientHandler()
            {
                UseDefaultCredentials = false,
                Credentials = CredentialCache.DefaultCredentials,
                AllowAutoRedirect = true
            };
            httpClient = new HttpClient(handler);
            httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(_configuration["BaseUrl"])
            };
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> GetDataFromAPIAsync<T>(string parameters)
        {
            try
            {
                var response = await httpClient.GetAsync(parameters);
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return default;
                }
                response.EnsureSuccessStatusCode();
                var body = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                return body;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}