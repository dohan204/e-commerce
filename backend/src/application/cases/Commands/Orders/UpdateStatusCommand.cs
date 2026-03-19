using MediatR;

namespace application.cases.Commands.Orders
{
    public class UpdateStatusCommand : IRequest<Unit>
    {
        public int orderId {get; set;}
        public int Status {get; set;}
    }
}