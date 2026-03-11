using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using application.interfaces;
using infrastructure.identity;
using Microsoft.Identity.Client;

namespace infrastructure.persistence.entities
{
    public class ReviewEntity : IBase, ISoftDelete
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(UserId))]
        public Guid UserId { get; set; }
        public AppUser AppUser { get; set; }
        [ForeignKey(nameof(ProductEntityId))]
        public int ProductEntityId { get; set; }
        public ProductEntity ProductEntity { get; set; }
        public decimal? Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeleteAt {get; set;}
        public string Comment { get; set; } = string.Empty;
    }
}