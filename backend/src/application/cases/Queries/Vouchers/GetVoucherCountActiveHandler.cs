using application.interfaces;
using MediatR;

namespace application.cases.Queries.Vouchers
{
    public class GetVoucherCountActiveHandler : IRequestHandler<GetVoucherCountActive, int>
    {
        private readonly IVoucherRepository _vouchersRepository;
        public GetVoucherCountActiveHandler(IVoucherRepository voucherRepository)
        {
            _vouchersRepository = voucherRepository;
        }

        public async Task<int> Handle(GetVoucherCountActive query, CancellationToken token)
        {
            return await _vouchersRepository.GetCountVoucher();
        }
    }
}