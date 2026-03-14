using infrastructure.dependency;
using infrastructure.persistence.entities;

namespace Integration.Testing
{
    public class SeedData
    {
        private static List<ProductEntity> GetProducts()
        {
            return new List<ProductEntity>()
            {
                new ProductEntity {Id = 1, Name = "Iphone 7", Description = "K co", Price = 2000, SalePrice = 150,Stock = 10, CategoryId = 1},
                new ProductEntity {Id = 2, Name = "Samsing 7", Description = "kh co cai gi ca", Price = 240, SalePrice = 50,Stock = 10, CategoryId = 1},
                new ProductEntity {Id = 3, Name = "Oppo 7", Description = "K co", Price = 2000, SalePrice = 150,Stock = 10, CategoryId = 1},
            };
        }
        public static void InitializeTestDb(ApplicationDbContext _context) 
        {
            if(!_context.Categories.Any())
            {
                var category = new CategoryEntity {Id = 1, Name = "Phone", Slug = "phone"};
                _context.Categories.Add(category);
                _context.SaveChanges();
            }
            _context.Products.AddRange(GetProducts());
            _context.SaveChanges();
        }
    }
}