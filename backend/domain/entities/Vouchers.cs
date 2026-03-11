namespace domain.entities
{
    public enum DiscountTypes
    {
        Percentage,
        FixedAmount
    }
    public class Voucher : BaseEntity
    {
        public string Code {get; set;} = string.Empty;
        public string DiscountType {get; set;} = string.Empty;
        public decimal Value {get; set;} = 0;
        public decimal MinOrder {get; set;} = 0;
        public int MaxUsage {get; set;} = 0;
        public DateTime ExpiryDate {get; set;}
        private Voucher(){}
        public Voucher(string discountType, decimal value, decimal minOrder, int maxUsage, DateTime expiryDate)
        {
            Code = Guid.NewGuid().ToString().Substring(0, 12);
            DiscountType = discountType;
            Value = value;
            MinOrder = minOrder;
            MaxUsage = maxUsage;
            ExpiryDate = expiryDate;
        }

        public static Voucher Create(string discountType, decimal value, decimal minOrder, int maxUsage, DateTime expiryDate) 
        => new Voucher(discountType, value, minOrder, maxUsage, expiryDate);

        public static decimal CalculateDiscountVouchers(decimal total, decimal value, DiscountTypes typeDiscount, decimal? maxDiscount = null)
        {
            // khởi tạo ban đầu
            decimal amountDiscount = 0; 
            // check kiểu giảm giá
            if(typeDiscount == DiscountTypes.Percentage)
            {
                // tính số tiền được giảm giá
                amountDiscount = total * (value / 100);
                if(maxDiscount.HasValue && amountDiscount > maxDiscount.Value) 
                   amountDiscount = maxDiscount.Value;
            }
            // giảm giá theo số tiền cố định
            if(typeDiscount == DiscountTypes.FixedAmount)
            {
                amountDiscount = value;
            }
            // trả ra số tiền được giảm giá và chỉ lấy ra 2 số sau dấu chấm
            return Math.Round(amountDiscount, 2);
        }
    }
}