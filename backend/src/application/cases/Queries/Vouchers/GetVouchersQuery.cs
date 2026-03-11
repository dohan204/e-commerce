using System.Windows.Input;
using MediatR;
using domain.entities;
namespace application.cases.Queries.Vouchers
{
    public class GetVouchersQuery : IRequest<IReadOnlyCollection<Voucher>>
    {
        
    }
}