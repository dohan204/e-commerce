using infrastructure.identity;
using Microsoft.Identity.Client;

namespace infrastructure.persistence.entities
{
    public class ReviewEntity : IBase
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public AppUser AppUser { get; set; }
        public int ProductEntityId { get; set; }
        public ProductEntity ProductEntity { get; set; }
        public decimal? Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}