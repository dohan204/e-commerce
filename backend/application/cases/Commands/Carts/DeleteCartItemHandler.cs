using application.exceptions;
using application.interfaces;
using domain.entities;
using MediatR;

namespace application.cases.Commands.Carts
{
    public class DeleteCartItemHandler : IRequestHandler<DeleteCartItemCommand, Unit>
    {
        private readonly ICartRepository cartRepository;
        public DeleteCartItemHandler(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }
        public async Task<Unit> Handle(DeleteCartItemCommand command, CancellationToken token)
        {
            var deleteCartItem = await cartRepository.DeleteAsync(command.UserId, command.ProductId);
            if(!deleteCartItem) 
                throw new NotFoundException($"Not found product with id: {command.ProductId}");
            return Unit.Value;
        }
    }
}