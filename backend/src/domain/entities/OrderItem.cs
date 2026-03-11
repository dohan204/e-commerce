using domain.exceptions;

namespace domain.entities
{
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; private set; }
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }  
        protected OrderItem() {}

        private OrderItem(int orderId, int productId, int quantity, decimal price)
        {
            // if(orderId <= 0) 
            //     throw new DomainException("Invalid Order");

            if(productId <= 0) 
                throw new DomainException("Invalid Product");
            
            if (quantity <= 0)
                throw new DomainException("Quantity must be than more 0");
            
            if(price <= 0) 
                throw new DomainException("Invalid Price");
            
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        } 
        public OrderItem(int Productid, int quantity, decimal price)
        {
            ProductId = Productid;
            Quantity = quantity;
            Price = price;
        }
        public static OrderItem Create(int orderId, int productId, int quantity, decimal price)
        => new OrderItem(orderId, productId, quantity, price);

        public static OrderItem Update(int productId, int quantity, decimal price) 
        => new OrderItem(productId, quantity, price);
    }
}