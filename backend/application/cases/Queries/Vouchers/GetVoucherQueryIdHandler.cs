using application.exceptions;
using application.interfaces;
using domain.entities;
using MediatR;
using Serilog;

namespace application.cases.Queries.Vouchers
{
    public class GetVoucherQueryIdHandler : IRequestHandler<GetVoucherQueryId, Voucher>
    {
        private readonly IVoucherRepository _repository;
        public GetVoucherQueryIdHandler(IVoucherRepository voucherRepository)
        {
            _repository = voucherRepository;
        }    
        public async Task<Voucher> Handle(GetVoucherQueryId query, CancellationToken token)
        {
            var voucher = await _repository.GetByIdAsync(query.Id);
            Log.Information("Bắt đầu tìm kiếm voucher..");
            if(voucher is null) 
                throw new NotFoundException($"Not found Voucher with id: {query.Id}");

            Log.Information("Thành công, trả về voucher..");

            return voucher;
        }
    }
}