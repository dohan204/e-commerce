using domain.entities;
using MediatR;

namespace application.cases.Queries.Users
{
    public class GetOrdersUserQuery : IRequest<IEnumerable<Order>>
    {
        public Guid UserId {get; set;}
    }
}