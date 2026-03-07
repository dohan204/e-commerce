using System.Linq.Expressions;
using System.Net.Http.Headers;
using application.interfaces;
using domain.entities;
using domain.enums;

namespace infrastructure.persistence.entities
{
    public class ProductEntity : IBase, ISoftDelete
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Slug { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal? SalePrice { get; set; }
        public int Stock { get; set; }
        public int? Sold { get; set; }
        public int CategoryId { get; set; }
        public CategoryEntity CategoryEntity { get; set; }
        public string? ImageUrl { get; set; }
        public decimal? AvgRating { get; set; }
        public int? ReviewCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public StatusProduct Status { get; set; } = StatusProduct.active;
        public ICollection<ReviewEntity> Reviews { get; set; } = new List<ReviewEntity>();
        public ICollection<OrderItemEntity> OrderItems { get; set; } = new List<OrderItemEntity>();
        public ICollection<CartItemEntity> Carts { get; set; } = new List<CartItemEntity>();
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeleteAt { get; set; }
    }
}