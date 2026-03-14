using MediatR;

namespace application.cases.Commands.Orders
{
    public class PaymentCommand : IRequest<Unit>
    {
        public int OrderID {get; set;}
    }
}