using MediatR;

namespace application.cases.Commands.Vouchers
{
    public class CreateVoucherCommand : IRequest<int>
    {
        public string DiscountType {get; set;} = string.Empty;
        public decimal Value {get; set;}
        public decimal MinOrder {get; set;}
        public int MaxUsage {get; set;}
        public DateTime ExpiryDate {get; set;}
    }
}