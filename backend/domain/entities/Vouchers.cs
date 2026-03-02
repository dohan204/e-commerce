namespace domain.entities
{
    public class Vouchers : BaseEntity
    {
        public string Code {get; set;} = string.Empty;
        public string DiscountType {get; set;} = string.Empty;
        public decimal Value {get; set;} = 0;
        public decimal MinOrder {get; set;} = 0;
        public int MaxUsage {get; set;} = 0;
        public DateTime ExpiryDate {get; set;}
    }
}