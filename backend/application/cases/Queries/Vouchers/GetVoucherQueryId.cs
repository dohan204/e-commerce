using domain.entities;
using MediatR;

namespace application.cases.Queries.Vouchers
{
    public class GetVoucherQueryId : IRequest<Voucher>
    {
        public required int Id {get; set;}
    }
}