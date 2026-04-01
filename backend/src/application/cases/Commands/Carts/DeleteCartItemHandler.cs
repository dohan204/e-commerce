using application.exceptions;
using application.interfaces;
using domain.entities;
using MediatR;
using Serilog;

namespace application.cases.Commands.Carts
{
    public class DeleteCartItemHandler : IRequestHandler<DeleteCartItemCommand, Unit>
    {
        private readonly ICartRepository cartRepository;

        private readonly ICurrentUser currentUser;
        public DeleteCartItemHandler(ICartRepository cartRepository, ICurrentUser currentUser)
        {
            this.cartRepository = cartRepository;
            this.currentUser = currentUser;
        }
        public async Task<Unit> Handle(DeleteCartItemCommand command, CancellationToken token)
        {
            if(!Guid.TryParse(currentUser.UserId, out var userid))
            {
                throw new UnauthorizeException("Người dùng không hợp lệ");
            }
            var deleteCartItem = await cartRepository.DeleteAsync(userid, command.ProductId);
            if(!deleteCartItem) 
                throw new NotFoundException($"Not found product with id: {command.ProductId}");

            Log.Information("Xóa sản phẩm trong giỏ hàng thành công");
            return Unit.Value;
        }
    }
}