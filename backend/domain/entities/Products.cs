using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using domain.enums;
using domain.exceptions;
namespace domain.entities
{
    
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; private set; } = string.Empty;
        public string Slug { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public decimal Price { get; private set; }
        public decimal? SalePrice { get; private set; }
        public int Stock {get; private set;} = 0;
        public int? Sold {get; private set;} = 0; // số lượng đẫ bán 
        public int CategoryId {get; private set;}
        public string? Images { get; private set; }
        public decimal? AvgRating { get; private set; }
        public int? ReviewCount { get; private set; }
        public StatusProduct Status { get; set; } = StatusProduct.active;
        public DateTime Created_At {get; set;}
        protected Products() {}
        private Products(
            string name, 
            string description, 
            decimal price, 
            int stock,
            int categoryId
            )
        {
            if(string.IsNullOrEmpty(name))
            {
                throw new DomainException("name is required");
            }
            if(price <= 0)
            {
                throw new DomainException("product price must be than 0");
            }
            if(stock <= 0)
            {
                throw new DomainException("Stock must be than more 0");
            }
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.Stock = stock;
            this.Sold = 0;
            this.CategoryId = categoryId;
            this.Images = string.Empty;
            this.AvgRating = 0;
            this.ReviewCount = 0;
            this.SalePrice = 0;
            this.Slug = GenerateSlug(name);
            this.Created_At = DateTime.Now;

        }
        public static Products Create(
            string name, 
            string description,
            decimal price,
            int stock, 
            int categoryid
        )
        {
            return new Products(name, description, price, stock, categoryid);
        }
        private string GenerateSlug(string input)
        {
            return input.ToLowerInvariant().Replace(" ", "-");
        }
    }
}
 