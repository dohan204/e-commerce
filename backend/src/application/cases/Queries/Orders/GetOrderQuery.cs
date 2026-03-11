using domain.entities;
using MediatR;

namespace application.cases.Queries.Orders
{
    public class GetOrderQuery : IRequest<Order>
    {
        public int Id { get; set; }
    }
}