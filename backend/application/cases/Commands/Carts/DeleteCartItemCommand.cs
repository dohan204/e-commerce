using MediatR;

namespace application.cases.Commands.Carts
{
    public class DeleteCartItemCommand : IRequest<Unit>
    {
        public Guid UserId {get; set;}
        public int ProductId {get; set;}
    }
}