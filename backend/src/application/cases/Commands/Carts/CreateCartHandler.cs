using application.interfaces;
using MediatR;
using domain.entities;
using Serilog;
using System.Runtime.InteropServices;
using application.exceptions;
namespace application.cases.Commands.Carts
{
    public class CreateCartHandler : IRequestHandler<CreateCartCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICurrentUser _user;
        private readonly ICartRepository _cartRepository;
        public CreateCartHandler(ICartRepository cartRepository, ICurrentUser user, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _user = user;
            _productRepository = productRepository;
        }
        public async Task<Unit> Handle(CreateCartCommand command, CancellationToken token)
        {
            if (!Guid.TryParse(_user.UserId, out var userId))
                throw new UnauthorizeException("User Invalid");

            var cart = await _cartRepository.GetCartByUserAsync(userId);
            // tim kiems san pham xem duoc dua vao co ton tai hay khong
            var product = await _productRepository.GetProductById(command.ProductId);

            if (product is null)
            {
                Log.Warning($"Khong tim thay san pham voi id: {command. ProductId}");
                throw new NotFoundException("Product is null");
            }
            if (cart is null)
            {
                Log.Warning($"Khong tim thay nguoi dung va gio hang, tien  hanh tao moi");
                cart = Cart.Create(userId);
                Log.Information("thucjw hien them cart vafo gio hang");
                cart.AddOrUpdate(command.ProductId, command.Quantity, product.Price);
                await _cartRepository.CreateAsync(cart);
            }
            else
            {
                cart.AddOrUpdate(command.ProductId, command.Quantity, command.Price);
                Log.Information("Cap nhat so luong trong gio hang");
                await _cartRepository.UpdateAsync(cart);
            }
            Log.Information("thanh cong");
            return Unit.Value;
        }
    }
}