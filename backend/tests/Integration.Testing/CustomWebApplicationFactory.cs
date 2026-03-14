using System.Data.Common;
using application.interfaces;
using infrastructure.dependency;
using infrastructure.persistence.entities;
using Integration.Testing.Mocks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Integration.Testing
{
    // tạo servertest cho các inrtegration 
    public class CustomWebApplicationFactory<TProgram>
        : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.ConfigureServices(services =>
            {
                // 1. Tìm và xóa TOÀN BỘ các đăng ký liên quan đến ApplicationDbContext
                // 1. Tìm và xóa toàn bộ các cấu hình DbContext cũ
                var descriptors = services.Where(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>) ||
                         d.ServiceType == typeof(IDbContextOptionsConfiguration<ApplicationDbContext>))
                    .ToList();

                foreach (var descriptor in descriptors)
                {
                    services.Remove(descriptor);
                }

                // 2. Xóa các service nội bộ của EF Core (để tránh xung đột Provider)
                var efInternalServices = services.Where(d =>
                    d.ServiceType.FullName?.StartsWith("Microsoft.EntityFrameworkCore") == true)
                    .ToList();

                foreach (var service in efInternalServices)
                {
                    services.Remove(service);
                }


                services.AddSingleton<DbConnection>(container =>
                {
                    var connection = new SqliteConnection("DataSource=:memory:");
                    connection.Open();

                    return connection;
                });

                services.AddDbContext<ApplicationDbContext>((container, options) =>
                {
                    var connection = container.GetRequiredService<DbConnection>();
                    options.UseSqlite(connection);
                });

            });
            builder.UseEnvironment("Development");
        }
    }
}