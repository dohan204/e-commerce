using application.cases.Dtos;
using application.interfaces;
using AutoMapper;
using domain.entities;
using domain.enums;
using infrastructure.dependency;
using infrastructure.persistence.entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
                .Select(p => new ProductViewDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    SalePrice = (decimal)p.SalePrice,
                    ImageUrl = p.ImageUrl,
                    AvgRating = (decimal)p.AvgRating,
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

            _ctx.Products.Remove(product);
            return true;
        }
        public async Task<bool> UploadImage(IFormFile file)
        {

            return true;
        }
    }
}