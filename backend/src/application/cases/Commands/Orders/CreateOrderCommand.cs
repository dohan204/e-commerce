using domain.entities;
using MediatR;

namespace application.cases.Commands.Orders
{
    public class CreateOrderCommand : IRequest<Unit>
    {
        public string OrderCode {get; set;} = string.Empty;
        public Guid UserId {get; set;}
        public ICollection<CreateOrderItem> OrderItems {get; set;} = new List<CreateOrderItem>();
        public string Status {get; set;} = string.Empty;
        public string PaymentStatus {get; set;} = string.Empty;
        public string PaymentMethod {get; set;} = string.Empty;
        public string ShippingAddress {get; set;} = string.Empty;
        public string? Note {get; set;} = string.Empty;
        public int? VoucherId {get; set;}
    }

    public class CreateOrderItem : IRequest<Unit>
    {
        public int ProductId {get; set;}
        public int OrderId {get; set;}
        public int Quantity {get; set;}
        public decimal Price {get; set;}
    }
}