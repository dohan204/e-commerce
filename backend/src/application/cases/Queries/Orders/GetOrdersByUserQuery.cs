using MediatR;
using domain.entities;
namespace application.cases.Queries.Orders
{
    public class GetOrdersByUserQuery : IRequest<IEnumerable<Order>>
    {
        public Guid UserId { get; set; }
        public GetOrdersByUserQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}