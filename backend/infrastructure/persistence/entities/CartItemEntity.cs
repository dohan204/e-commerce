using application.interfaces;
using Microsoft.Identity.Client;

namespace infrastructure.persistence.entities
{
    public class CartItemEntity : BaseEntity, ISoftDelete
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public CartEntity Cart {get; set;}
        public int ProductId {get; set;}
        public virtual ProductEntity Product {get; set;}
        public int Quantity {get; set;}
        public decimal UnitPrice {get; set;}
        public bool IsDeleted {get; set;}
        public DateTimeOffset? DeleteAt {get; set;}
    }
}