namespace infrastructure.persistence.entities
{
    public class OrderItemEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public OrderEntity Orders {get; set;}
        public int ProductId {get; set;}
        public ProductEntity Products {get; set;}
        public int Quantity {get; set;}
        public int Price {get; set;}
    }
}