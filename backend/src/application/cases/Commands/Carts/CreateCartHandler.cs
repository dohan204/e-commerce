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
                Log.Warning($"Không tìm thấy người dùng và giỏ hàng, tiến hành tạo mới");
                cart = Cart.Create(userId);
                Log.Information("thực hiện thêm sản phẩm vào giỏ hàng.");
                cart.AddOrUpdate(command.ProductId, command.Quantity, product.Price);
                await _cartRepository.CreateAsync(cart);


                Log.Information("Thêm sản phẩm vào đơn hàng thành công: {id}-{quantity}-{price}", product.Id, command.Quantity, product.Price);
            }
            else
            {
                cart.AddOrUpdate(command.ProductId, command.Quantity, product.Price);
                Log.Information("Cập nhật số lượng trong giỏ hàng.");
                await _cartRepository.UpdateAsync(cart);
                Log.Information("Cập nhật Thành công.");
                Log.Information("Thêm sản phẩm vào đơn hàng thành công: {id}-{quantity}-{price}", command.ProductId, command.Quantity, product.Price);
            }
            Log.Information("thêm thành công.");
            return Unit.Value;
        }
    }
}