using MediatR;

namespace application.cases.Commands.Orders
{
    public class UpdateOrderCommand : IRequest<Unit>
    {
        public int OrderId {get; set;}
        public Guid UserId {get; set;}
        public decimal DiscountAmount {get; set;}
        public decimal ShippingFee {get; set;}
        public decimal TotalAmount {get;set;}
        public ICollection<UpdateOrderItemCommand>  UpdateOrderItem {get; set;} = new List<UpdateOrderItemCommand>();   
        public string Status {get; set;} = string.Empty;
        public string PaymentMethod {get; set;} = string.Empty;
        public string PaymentStatus {get;set; } = string.Empty;
        public string? ShippingAddress {get; set;} = string.Empty;
        public string? Note {get; set;}
    }
    public class UpdateOrderItemCommand
    {
        public int OrderId {get;}
        public int ProductId {get; set;}
        public int Quantity {get; set;}
        public decimal Price {get; set;}
    }
}