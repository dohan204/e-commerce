using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [ForeignKey(nameof(ProductId))]
        public int ProductId {get; set;}
        public ProductEntity ProductEntity{ get; set; }

        public int Quantity {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime? UpdatedAt {get; set;}
    }
}