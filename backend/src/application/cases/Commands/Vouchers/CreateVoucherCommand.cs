using domain.entities;
using MediatR;

namespace application.cases.Commands.Vouchers
{
    public class CreateVoucherCommand : IRequest<int>
    {
        public DiscountTypes DiscountType {get; set;}
        public decimal Value {get; set;}
        public decimal MinOrder {get; set;}
        public int MaxUsage {get; set;}
        public DateTime ExpiryDate {get; set;}
    }
}