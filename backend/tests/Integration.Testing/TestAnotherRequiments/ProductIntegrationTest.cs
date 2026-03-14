using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using api.Helpers.Dtos;
using application.cases.Commands.Product;
using application.cases.Dtos;
using domain.entities;
using infrastructure.dependency;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Org.BouncyCastle.Math.EC;
using Xunit.Abstractions;

namespace Integration.Testing.TestAnotherRequiments
{
    public class ProductIntegrationTests : BaseIntegrationTest
    {
        // private readonly HttpClient _client;
        private readonly ITestOutputHelper _output;
        // private readonly CustomWebApplicationFactory<Program> _factory;
        public ProductIntegrationTests(CustomWebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper, IHttpContextAccessor httpContextAccessor) : base(factory, httpContextAccessor)
        {
            // _factory = factory;
            // _client = _factory.CreateClient();
            _output = testOutputHelper;
        }
        [Fact]
        public async Task GetAllProducts_ReturnListAndSuccess()
        {
            // Setup và thiết lập data ban đầu \
            using (var scope = _factory.Services.CreateScope())
            {
                var scopeService = scope.ServiceProvider;
                var db = scopeService.GetRequiredService<ApplicationDbContext>();

                // sử dụng cái này tạo bảng cho sqlite sử dụng inmemoryData
                db.Database.EnsureCreated();
                // db.Database.Migrate();

                SeedData.InitializeTestDb(db);
            }
            // Act
            var response = await _client.GetAsync("/api/product");
            var jsonString = await response.Content.ReadAsStringAsync();

            // In ra màn hình console của Test
            _output.WriteLine("--- NỘI DUNG JSON TRẢ VỀ ---");
            _output.WriteLine(jsonString);
            _output.WriteLine("----------------------------");

            // Assert
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<List<ProductViewDto>>>();
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal(3, result.Data.Count); // Kiểm tra xem có đúng 3 con Iphone, Samsung, Oppo không
            Assert.Equal("Iphone 7", result.Data.First().Name);
        }

        [Fact]
        public async Task GetById_ReturnProductWithSuccess()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var scopeService = scope.ServiceProvider;
                var db = scopeService.GetRequiredService<ApplicationDbContext>();

                // sử dụng cái này tạo bảng cho sqlite sử dụng inmemoryData
                db.Database.EnsureCreated();
                // db.Database.Migrate();
                SeedData.InitializeTestDb(db);
            }

            // act 
            var response = await _client.GetAsync("api/product/1");
            var error = await response.Content.ReadAsStringAsync();
            _output.WriteLine(error);
            var result = await response.Content.ReadFromJsonAsync<ProductViewDto>();
            // assert: 
            response.EnsureSuccessStatusCode(); // trả về 200 khi có dữ liệu thành công
            Assert.NotNull(result);
            Assert.True(result.Id > 0);
            Assert.Equal(1, result.Id);
            Assert.Equal("Iphone 7", result.Name);
            _output.WriteLine($"Đã tìm thấy sản phẩm: {result.Name}");
        }

        [Fact]
        public async Task Handle_CreateProductSuccess_ReturnStatusCreated()
        {
            using(var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                db.Database.EnsureCreated();

                SeedData.InitializeTestDb(db);
            }
            var token = await GetJwtTokenAsync("dohan","dohan205@gmail.com" , "Han2005@");
            var newProduct = new CreateProductCommand
            {
                Name = "Iphone 8",
                Description = "Không có gì để nói",
                Price = 3000,
                Stock = 20,
                CategoryId = 1
            };

            _client.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", token);
            // act 
            var response = await _client.PostAsJsonAsync("/api/product", newProduct);

            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);

            var result = await response.Content.ReadFromJsonAsync<ApiResponse<ProductViewDto>>();
            Assert.NotNull(result);

            _output.WriteLine($"Tạo thành công sản phẩm với ID: {result.Data.Id}");
        }
    }
}