using System.Net.Http.Headers;
using application.cases.Dtos;
using domain.entities;
using Microsoft.AspNetCore.Http;

namespace application.interfaces
{
    public class TopProductSale
    {
        public string Name { get; set; }
        public int? Quantity { get; set; }
    }
    public interface IProductRepository
    {
        Task<IEnumerable<ProductViewDto>> GetProductsAsync();
        Task<Products?> GetProductById(int id);
        Task AddAsync(Products products);
        Task UpdateAsync(Products products);
        Task<bool> DeleteAsync(int Id);
        // Task<bool> UploadImage(int productId,IFormFile file);
        Task<object?> GetFullDashboardStatsAsync();

        Task<IEnumerable<TopProductSale>> GetTopProductSalesAsync();
    }
}