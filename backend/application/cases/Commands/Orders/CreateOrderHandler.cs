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
        public CreateOrderHandler(IOrderRepository orderRepository, ICurrentUser user, IProductRepository repoProduct, IVoucherRepository voucherRepository)
        {
            _repository = orderRepository;
            currentUser = user;
            _repoProduct = repoProduct;
            _voucherRepository = voucherRepository;
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
                var create = OrderItem.Create
                (
                    order.Id,
                    product.Id,
                    item.Quantity,
                    item.Price
                );
                order.Items.Add(create);
            }
            // tính tổng số tiền trong đơn hàng 

            var totalAmount = order.Items.Sum(x => x.Quantity * x.Price);
            // trả ra số tiền cuối cùng

            if(order.VoucherId.HasValue)
            {
                var voucher = await _voucherRepository.GetByIdAsync(order.VoucherId ?? 0);
                if(voucher is null) 
                    throw new NotFoundException("Voucher is not found");
                var discountAmount = Voucher.CalculateDiscountVouchers(totalAmount, voucher.Value, DiscountTypes.Percentage);
                order.ApplyDiscount(discountAmount);
                order.ReturnFinal(totalAmount - discountAmount);
            }
            Log.Information("Start insert Db,");
            await _repository.CreateAsync(order);
            return Unit.Value;
        }
    }
}