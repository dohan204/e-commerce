using domain.exceptions;

namespace domain.entities
{
    public class Cart : BaseEntity
    {
        public Guid UserId { get; private set; }
        public int TotalQuantity => _cart.Sum(e => e.Quantity);
        public decimal TotalPrice => _cart.Sum(e => e.Quantity * e.UnitPrice);
        private readonly List<CartItem> _cart = new ();
        public IReadOnlyCollection<CartItem> Items => _cart;
        // public int QuantityProduct { get;private set; }
        protected Cart() {}
        private Cart(Guid userId)
        {
            if(userId == Guid.Empty)
            throw new DomainException("User invlaid");
            
            UserId = userId;
        }

        public static Cart Create(Guid userId) 
        => new Cart(userId);

        public void AddOrUpdate(int productId, int quantity, decimal price)
        {
            var existItem = _cart.FirstOrDefault(e => e.ProductId == productId);
            if(existItem is not null)
            {
                existItem.Increment(quantity);
            } else
            {
                _cart.Add(new CartItem(productId, quantity, price));
            }
        }
        public void DeleteProduct(int productID)
        {
            _cart.RemoveAll(e => e.ProductId == productID);
        }
    }
}