using application.exceptions;
using application.interfaces;
using domain.entities;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace application.cases.Commands.Orders
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Unit>
    {
        private readonly ICurrentUser currentUser;
        private readonly IOrderRepository _repository;
        private readonly IProductRepository _repoProduct;
        private readonly IVoucherRepository _voucherRepository;
        private readonly IAddressRepository _addressRepository;
        public CreateOrderHandler(IOrderRepository orderRepository, ICurrentUser user, IProductRepository repoProduct, 
        IVoucherRepository voucherRepository, IAddressRepository addressRepository)
        {
            _repository = orderRepository;
            currentUser = user;
            _repoProduct = repoProduct;
            _voucherRepository = voucherRepository;
            _addressRepository = addressRepository;
        }

        public async Task<Unit> Handle(CreateOrderCommand command, CancellationToken token)
        {
            
            if(Guid.TryParse(currentUser.UserId, out var user))
            {
                Log.Information($"Infor user: {user}");
            }
            // Log.Information("Start create Order with User and product", new {user = user, product = command.OrderCode});
            var order =  Order.Create(
                user,
                command.ShippingAddress,
                command.VoucherId > 0 ? command.VoucherId : null,
                command.Note
            );

            foreach(var item in command.OrderItems)
            {
                var product = await _repoProduct.GetProductById(item.ProductId);
                if(product is null)
                {
                    Log.Warning($"Product with id: {item.ProductId} Not found");
                    throw new NotFoundException("Product not found");
                }
                if(product.Stock < item.Quantity)
                {
                    Log.Warning("Số lượng sản phẩm không đủ");
                    throw new BussinesErrorException("Số lượng đặt vượt quá số lượng sản phẩm hiện có");
                }
                order.AddOrderItem(product.Id, item.Quantity, product.Price);
            }
            if(order.VoucherId.HasValue)
            {
                var voucher = await _voucherRepository.GetByIdAsync(order.VoucherId ?? 0);
                if(voucher is null) 
                    throw new NotFoundException("Voucher is not found");
                    
                var discountAmount = Voucher.CalculateDiscountVouchers(order.TotalAmount, voucher.Value, DiscountTypes.Percentage);
                order.ApplyDiscount(discountAmount);
            }

            order.RecalculateAmount();

            // lấy ra địa chỉ của người dùng 
            var addressUser = await _addressRepository.GetByUserId(user);
            
            // tiến hành convert để chuyển nó vào địa chỉ giao hàng
            order.SetAddressToOrder(addressUser.ToString());
            Log.Information("Start insert Db,");
            await _repository.CreateAsync(order);
            return Unit.Value;
        }
    }
}