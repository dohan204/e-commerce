using domain.entities;
using MediatR;

namespace application.cases.Queries.Orders
{
    public class GetOrdersQuery : IRequest<IReadOnlyList<Order>> {}
}