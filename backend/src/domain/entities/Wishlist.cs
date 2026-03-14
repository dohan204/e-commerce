namespace domain.entities
{
    public class Wishlist : BaseEntity
    {
        public Guid Userid {get; set;}
        public int ProductId {get; set;}
        public DateTime CreatedAt {get; set;}
        public Wishlist() {}
    }
}