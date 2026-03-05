using application.interfaces;
using domain.enums;
using infrastructure.identity;

namespace infrastructure.persistence.entities
{
    public class OrderEntity : IBase, ISoftDelete
    {
        public int Id { get; set;}
        public string OrderCode {get; set;} = string.Empty;
        public Guid UserId { get; set;}
        public AppUser AppUser{ get; set;}
        public decimal TotalAmount {get; set;}  // tổng tiền đơn hàng 
        public decimal? DiscountAmount {get; set;} = decimal.Zero;
        public decimal? ShippingFee {get; set;} = decimal.Zero;
        public decimal FinalAmount {get; set;} = 0; // số tiền phải trả 
        public StatusOrdere StatusOrdere {get; set;}
        public PaymentMethodOrder PaymentMethod {get; set;}
        public PaymentStatusOrder PaymentStatus {get; set;} = PaymentStatusOrder.unpaid;
        public string ShippingAdress {get; set;} = string.Empty;
        public int? VoucherId {get; set;}
        public VoucherEntity Voucher {get; set;}
        public string? Note {get; set;}
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted {get; set;}
        public DateTimeOffset? DeleteAt {get; set;}
        public ICollection<OrderItemEntity> Items {get; set;} = new List<OrderItemEntity>();
    }
}