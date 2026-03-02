using domain.exceptions;

namespace domain.entities
{
    public class Cart : BaseEntity
    {
        public Guid UserId { get; private set; }
        public int ProductId { get;private set; }
        public int Quantity { get;private set; }
        protected Cart() {}
        private Cart(Guid userId, int productId, int quantity)
        {
            if(userId == Guid.Empty)
            throw new DomainException("User invlaid");
            if(productId <= 0) 
            throw new DomainException("Product in cart must than more 0");
            UserId = userId;
            ProductId = productId;
            Quantity = quantity;
        }

        public static Cart Create(Guid userId, int productId, int quantity) 
        => new Cart(userId, productId, quantity);

        public void Increment(int quantity)
        {
            if(quantity <= 0)
            throw new DomainException("quantity must be than 0");

            Quantity += quantity;
        }
        public void UpdateQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new DomainException("Quantity invalid");

            Quantity = quantity;
        }
    }
}