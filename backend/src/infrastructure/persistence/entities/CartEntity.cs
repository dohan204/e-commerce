using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using domain.entities;
using infrastructure.identity;

namespace infrastructure.persistence.entities
{
    public class CartEntity : IBase
    {
        [Key]
        public int Id {get; set;}
        [ForeignKey(nameof(UserId))]
        public Guid UserId { get; set; }
        public AppUser AppUser{ get; set; }
        public virtual ICollection<CartItemEntity> Items { get; set; } = new List<CartItemEntity>();
        public DateTime CreatedAt {get; set;}
        public DateTime? UpdatedAt {get; set;}
    }
}