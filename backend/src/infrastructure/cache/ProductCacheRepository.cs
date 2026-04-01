using application.cases.Dtos;
using application.helpers;
using application.interfaces;
using domain.entities;
using infrastructure.cache.helpers;
using infrastructure.repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace infrastructure.cache
{
    public class ProductCacheRepository : Helpers, IProductRepository
    {
        private readonly ProductRepository _productRepository;
        // private readonly MemoryCacheEntryOptions _options;
        public ProductCacheRepository(IMemoryCache cache, IConfiguration config, ProductRepository productRepository) : base(cache, config)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductViewDto>> GetProductsAsync()
        => await GetOrCreateAsync("products_all",
            () => _productRepository.GetProductsAsync()) ?? Enumerable.Empty<ProductViewDto>();

        public async Task<Products?> GetProductById(int id)
        => await GetOrCreateAsync($"product:{id}",
            () => _productRepository.GetProductById(id));

        public async Task AddAsync(Products product)
        {
            await _productRepository.AddAsync(product);
            RemoveCache("products_all");
            RemoveCache($"product:{product.Id}");
            RemoveCache("sales");
            RemoveByPrefix("pagination:");
        }
        public async Task UpdateAsync(Products product)
        {
            await _productRepository.UpdateAsync(product);
            RemoveCache("products_all");
            RemoveCache("sales");
            RemoveByPrefix("pagination:");
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
            RemoveCache("products_all");
            RemoveCache($"product:{id}");

            RemoveCache("sales");
            RemoveByPrefix("pagination:");
            return true;
        }
        public async Task<object?> GetFullDashboardStatsAsync()
        => await GetOrCreateAsync("datadashboard",
            () => _productRepository.GetFullDashboardStatsAsync());

        public async Task<IEnumerable<TopProductSale>> GetTopProductSalesAsync() =>
        await GetOrCreateAsync("topSales",
        () => _productRepository.GetTopProductSalesAsync()) ?? Enumerable.Empty<TopProductSale>();


        public async Task<PagedResult<ProductViewDto>> PaginationProduct(
     int page, int pageSize, string? search, int? categoryid)
        {
            var cacheKey = $"pagination:{page}:{pageSize}:{search}:{categoryid}";

            return await GetOrCreateAsync(cacheKey,
                () => _productRepository.PaginationProduct(page, pageSize, search, categoryid)
            ) ?? new PagedResult<ProductViewDto>();
        }
        public async Task<IEnumerable<ProductViewDto>> SearchProductAsync(string search) =>
        await GetOrCreateAsync($"search_product_{search}", () => _productRepository.SearchProductAsync(search)) ?? Enumerable.Empty<ProductViewDto>();

        public async Task<IEnumerable<ProductViewDto>> GetSales()
        => await GetOrCreateAsync("sales", () => _productRepository.GetSales()) ?? Enumerable.Empty<ProductViewDto>();
    }
}