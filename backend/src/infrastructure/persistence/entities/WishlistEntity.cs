using application.interfaces;
using infrastructure.identity;

namespace infrastructure.persistence.entities
{
    public class WishlistEntity : IBase
    {
        public int Id {get; set;}
        public Guid UserId {get; set;}
        public virtual AppUser User {get; set;}
        public int ProductId {get; set;}
        public ProductEntity Products {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime? UpdatedAt {get; set;}
    }
}