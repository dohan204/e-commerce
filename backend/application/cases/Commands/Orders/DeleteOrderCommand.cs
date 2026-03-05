using MediatR;

namespace application.cases.Commands.Orders
{
    public class DeleteOrderCommand : IRequest<Unit>
    {
        public int Id {get; set;}
    }
}