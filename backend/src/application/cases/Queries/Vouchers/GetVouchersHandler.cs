using application.interfaces;
using domain.entities;
using MediatR;
using Serilog;

namespace application.cases.Queries.Vouchers
{
    public class GetVouchersHandler : IRequestHandler<GetVouchersQuery, IReadOnlyCollection<Voucher>>
    {
        private readonly IVoucherRepository _repositoryVoucher;
        public GetVouchersHandler(IVoucherRepository voucherRepository)
        {
            _repositoryVoucher = voucherRepository;
        }
        public async Task<IReadOnlyCollection<Voucher>> Handle(GetVouchersQuery query, CancellationToken token)
        {
            var vouchers = await _repositoryVoucher.GetVouchersAsync();
            if(!vouchers.Any())
                Log.Warning("Danh sách Voucher trống");
            return vouchers;
        }
    }
}