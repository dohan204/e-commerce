using domain.entities;
using MediatR;

namespace application.cases.Queries.Carts
{
    public class GetCartByUserQuery : IRequest<Cart>
    {
        public Guid Userid {get; set;}
    }
}