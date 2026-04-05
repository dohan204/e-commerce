using application.cases.Dtos;
using application.helpers;
using application.interfaces;
using AutoMapper;
using domain.entities;
using domain.enums;
using infrastructure.dependency;
using infrastructure.persistence.entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Serilog;

namespace infrastructure.repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _ctx;
        public ProductRepository(ApplicationDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductViewDto>> GetProductsAsync()
        {
            return await _ctx.Products.AsNoTracking()
                .Include(e => e.Reviews)
                .Select(p => new ProductViewDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock,
                    Sold = p.Sold,
                    SalePrice = (decimal)p.SalePrice!,
                    CategoryId = p.CategoryId,
                    ImageUrl = p.ImageUrl,
                    ReviewCount = p.Reviews.Count(),
                    AvgRating = p.Reviews.Average(e => e.Rating),
                }).ToListAsync();
        }
        public async Task<Products?> GetProductById(int id)
        {
            var product = await _ctx.Products.FindAsync(id);
            return _mapper.Map<Products>(product);
        }
        public async Task AddAsync(Products products)
        {
            var productDb = _mapper.Map<ProductEntity>(products);
            _ctx.Products.Add(productDb);
            await _ctx.SaveChangesAsync();
            Log.Information("Create product successfully.");
        }
        public async Task UpdateAsync(Products product)
        {
            var entity = await _ctx.Products.FindAsync(product.Id);

            _mapper.Map(product, entity);

            await _ctx.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _ctx.Products.FindAsync(id);
            if (product is null)
                return false;

            product.IsDeleted = true;
            product.DeleteAt = DateTime.UtcNow;
            await _ctx.SaveChangesAsync();
            return true;
        }
        public async Task<object?> GetFullDashboardStatsAsync()
        {
            var stats = await _ctx.Products
                .AsNoTracking()
                .GroupBy(e => 1)
                .Select(e => new
                {
                    TotalProducts = e.Count(),
                    TotalStock = e.Sum(e => e.Stock),
                    TotalSold = e.Sum(e => e.Sold),
                    TotalRevenua = e.Sum(e => (decimal?)(e.Price * e.Sold)),
                }).FirstOrDefaultAsync();
            return stats;
        }

        public async Task<IEnumerable<TopProductSale>> GetTopProductSalesAsync()
        {
            var products = await _ctx.Products.AsNoTracking()
                .Include(e => e.Reviews)
                .GroupBy(e => e.Name)
                .Select(e => new TopProductSale
                {
                    Name = e.Key,
                    Quantity = e.Sum(e => e.Sold),
                })
                .OrderByDescending(e => e.Quantity)
                .ToListAsync();

            return products;
        }
        public async Task<PagedResult<ProductViewDto>> PaginationProduct(int page, int pageSize, string? search, int? categoryId)
        {
            var products = _ctx.Products.AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(search))
                products = products.Where(e => e.Name.Contains(search) || e.Tag.Contains(search));

            if (categoryId.HasValue)
                products = products.Where(e => e.CategoryId == categoryId.Value);

            var total = await products.CountAsync();

            var items = await products
            .Include(e => e.Reviews.Where(e => !e.IsDeleted))
                .OrderBy(e => e.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var dataMapping = _mapper.Map<IEnumerable<ProductViewDto>>(items);
            return new PagedResult<ProductViewDto>
            {
                Total = total,
                Items = dataMapping,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task<IEnumerable<ProductViewDto>> SearchProductAsync(string search)
        {
            var product = await _ctx.Products.AsNoTracking()
            .Where(e => e.Name.Contains(search) || e.CategoryEntity.Name.Contains(search))
            .ToListAsync();

            return _mapper.Map<IEnumerable<ProductViewDto>>(product);
        }

        public async Task<IEnumerable<ProductViewDto>> GetSales()
        {
            var products = await _ctx.Products.AsNoTracking()
                .OrderByDescending(e => e.Sold)
                .Take(15)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ProductViewDto>>(products);
        }


    }
}