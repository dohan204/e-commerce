using MediatR;

namespace application.cases.Commands.Product
{
    public class DeleteProductCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}