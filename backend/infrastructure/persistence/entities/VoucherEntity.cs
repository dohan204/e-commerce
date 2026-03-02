using domain.enums;
namespace infrastructure.persistence.entities
{
    public class VoucherEntity : IBase
    {
        public int Id { get; set;}
        public string Code { get; set;} = string.Empty; // tên mã gg, TET2026, ...
        public Voucher DiscountType { get; set; } // kiểu giảm giá, theo phần trăm hoặc số tiền cố định
        public decimal Value {get; set;} // số tiền hoặc phần trăm giảm 
        public int MinOrder {get; set;} // đơn tối thiểu để được áp dụng 
        public int MaxUsage {get; set;}
        public ICollection<OrderEntity> Orders { get; set; } = new List<OrderEntity>();
        public DateTime ExpiryDate {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime? UpdatedAt {get; set;}

    }
}