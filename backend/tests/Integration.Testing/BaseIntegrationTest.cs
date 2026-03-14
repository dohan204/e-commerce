using System.Net.Http.Json;
using application.cases.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Serilog;

namespace Integration.Testing
{
    public abstract class BaseIntegrationTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        protected readonly HttpClient _client;
        protected readonly IHttpContextAccessor httpContext;
        protected readonly CustomWebApplicationFactory<Program> _factory;
        public BaseIntegrationTest(CustomWebApplicationFactory<Program> factory, IHttpContextAccessor httpContext) 
        {
            _factory = factory;
            _client = _factory.CreateClient();
            this.httpContext = httpContext;
        }

        protected async Task<string> GetJwtTokenAsync(string username, string? email, string password)
        {
            // đăng ký user
            HttpResponseMessage registerResponse = await _client.PostAsJsonAsync("/api/user/create", new
            {
                UserName = username,
                Email = email,
                Password = password
            });

            if(!registerResponse.IsSuccessStatusCode)
            {
                var error = await registerResponse.Content.ReadAsStringAsync();
                throw new Exception($"Đăng ký thất bại{error}");
            }
            // gọi api để lấy token 
            var loginResponse = await _client.PostAsJsonAsync("/api/auth/login", new
            {
                UserName = username,
                Password = password
            });
            if(!loginResponse.IsSuccessStatusCode)
            {
                var error = await loginResponse.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
            var result = await loginResponse.Content.ReadFromJsonAsync<ResponseLogin>();
            return result.token;
        }
    }
}