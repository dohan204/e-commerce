using MediatR;

namespace application.cases.Commands.Carts
{
    public class CreateCartCommand : IRequest<Unit>
    {
        public int ProductId {get; set;}
        public int Quantity {get; set;}
        public decimal Price {get; set;}
    }
}