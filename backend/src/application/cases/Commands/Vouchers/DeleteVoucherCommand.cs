using MediatR;

namespace application.cases.Commands.Vouchers
{
    public class DeleteVoucherCommand : IRequest<bool>
    {
        public int Id {get; set;}
    }
}