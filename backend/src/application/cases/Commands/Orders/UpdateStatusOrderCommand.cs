using MediatR;

namespace application.cases.Commands.Orders
{
    public class UpdateStatusOrderCommand : IRequest<Unit>
    {
        public int OrderId {get; set;}
    }
}