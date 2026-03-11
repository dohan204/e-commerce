using application.interfaces;
using MediatR;

namespace application.cases.Commands.Product
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        public DeleteProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Unit> Handle(DeleteProductCommand command, CancellationToken token)
        {
            await _productRepository.DeleteAsync(command.Id);
            return Unit.Value;
        }
    }
}