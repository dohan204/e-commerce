using application.exceptions;
using application.interfaces;
using MediatR;
using Serilog;

namespace application.cases.Commands.Vouchers
{
    public class DeleteVoucherHandler : IRequestHandler<DeleteVoucherCommand, bool>
    {
        private readonly IVoucherRepository _vouchersRepository;
        public DeleteVoucherHandler(IVoucherRepository voucherRepository)
        {
            _vouchersRepository = voucherRepository;
        }

        public async Task<bool> Handle(DeleteVoucherCommand command, CancellationToken token)
        {
            var isDelete = await _vouchersRepository.DeleteAsync(command.Id);
            if(!isDelete)
            {
                Log.Warning($"Không tìm thấy voucher với Id: {command.Id}");
                throw new NotFoundException($"Không tìm thấy voucher với Id: {command.Id}");
            }
            return true;
        }
    }
}