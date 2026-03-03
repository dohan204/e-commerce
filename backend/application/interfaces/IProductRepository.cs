using System.Net.Http.Headers;
using application.cases.Dtos;
using domain.entities;

namespace application.interfaces
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<ProductViewDto>> GetProductsAsync();
        Task<Products?> GetProductById(int id);
        Task AddAsync(Products products);
        Task UpdateAsync(Products products);
        Task<bool> DeleteAsync(int Id);
    }
}