using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
        public int Stock { get; private set; } = 0;
        public int? Sold { get; private set; } = 0; // số lượng đẫ bán 
        public int CategoryId { get; private set; }
        public string? Images { get; private set; }
        public string? Tag { get; private set; }
        private readonly List<Review> _review = new();
        public IReadOnlyList<Review> Reviews => _review;
        public decimal AvgRating =>  _review.Average(e => e.Rating);      
        public int ReviewCount => _review.Count; 
        public StatusProduct Status { get; set; } = StatusProduct.active;
        public DateTime Created_At { get; set; }
        public Products() { }
        // public Products() {}c
        public Products(
            string name,
            string description,
            decimal price,
            int stock,
            int categoryId,
            string? images,
            string? tag
            )
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new DomainException("name is required");
            }
            if (price <= 0)
            {
                throw new DomainException("product price must be than 0");
            }
            if (stock <= 0)
            {
                throw new DomainException("Stock must be than more 0");
            }
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.Stock = stock;
            this.Sold = 0;
            this.CategoryId = categoryId;
            this.Images = images ?? string.Empty;
            this.SalePrice = 0;
            this.Slug = GenerateSlug(name);
            this.Created_At = DateTime.Now;
            Tag = tag;
        }
        public Products(
            int id,
            string name,
            string description,
            decimal price,
            int stock,
            int categoryId,
            string imageUrl,
            string? tag
        )
        {
            Id = id;
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.Stock = stock;
            this.Sold = 0;
            this.CategoryId = categoryId;
            this.Images = imageUrl;
            this.SalePrice = 0;
            this.Slug = GenerateSlug(name);
            this.Created_At = DateTime.Now;
            this.Tag = tag;
        }
        public static Products Create(
            string name,
            string description,
            decimal price,
            int stock,
            int categoryid,
            string imageUrl,
            string? tag
        )
        {
            return new Products(name, description, price, stock, categoryid, imageUrl, tag);
        }
        public void Update(string? name
        , string? description,
        decimal? price,
        int? stock,
        int? sold,
        decimal? salePrice,
        int? categoryId,
        string filePath)
        {
            this.Name = name!;
            this.Description = description;
            this.Price = (decimal)price!;
            this.Stock = (int)stock!;
            this.CategoryId = (int)categoryId;
            this.Sold = sold;
            this.SalePrice = salePrice;
            this.Images = filePath;
        }
        private string GenerateSlug(string input)
        {
            return input.ToLowerInvariant().Replace(" ", "-");
        }
        public void UpdateImage(string filePath)
        {
            Images = filePath;
        }

        public void UpdateStock(int stock)
        {
            Stock -= stock;
            Sold += stock;
        }
        public void RestoreStock(int stock)
        {
            Stock += stock;
            Sold -= stock;
        }

        public void AddReview(Review review)
        {
            if (review is null)
                throw new DomainException("Review cannot be null");

            if (_review.Any(e => e.UserId == review.UserId))
            {
                throw new DomainException("User already reviewd this product");
            }

            _review.Add(review);

            // UpdateAvgRating();
        }
    }
}
