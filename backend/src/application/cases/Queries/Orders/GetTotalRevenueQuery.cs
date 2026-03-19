using application.interfaces;
using MediatR;

namespace application.cases.Queries.Orders
{
    public class GetTotalRevenueQuery : IRequest<IEnumerable<RevenueDto>> {}
}