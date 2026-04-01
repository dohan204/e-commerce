using System.Reflection.Metadata;
using application.cases.Commands.Notifications;
using application.exceptions;
using application.interfaces;
using application.services;
using domain.entities;
using MediatR;
using Microsoft.AspNetCore.Razor.TagHelpers;
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
        private readonly INotificationService _notificationService;
        private readonly IMediator _mediator;
        public CreateOrderHandler(IOrderRepository orderRepository, ICurrentUser user, IProductRepository repoProduct,
        IVoucherRepository voucherRepository, IAddressRepository addressRepository, INotificationService notificationService, IMediator mediator)
        {
            _repository = orderRepository;
            currentUser = user;
            _repoProduct = repoProduct;
            _voucherRepository = voucherRepository;
            _addressRepository = addressRepository;
            _notificationService = notificationService;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(CreateOrderCommand command, CancellationToken token)
        {

            if (!Guid.TryParse(currentUser.UserId, out var user))
                throw new UnauthorizeException("Invalid user");
            // Log.Information("Start create Order with User and product", new {user = user, product = command.OrderCode});
            // lấy ra địa chỉ của người dùng 
            var addressUser = await _addressRepository.GetByUserId(user);
            if (addressUser == null)
            {
                Log.Warning("User {UserId} attempted to create order without address", user);
                throw new NotFoundException("Địa chỉ giao hàng không tồn tại. Vui lòng cập nhật địa chỉ.");
            }
            // tiến hành convert để chuyển nó vào địa chỉ giao hàng
            // order.SetAddressToOrder(addressUser.AddressFull());
            var order = Order.Create(
                user,
                addressUser.AddressFull(),
                command.VoucherId > 0 ? command.VoucherId : null,
                (PaymentMethod)command.PaymentMethod!,
                command.Note
            );

            foreach (var item in command.OrderItems)
            {
                var product = await _repoProduct.GetProductById(item.ProductId);
                if (product is null)
                {
                    Log.Warning($"Product with id: {item.ProductId} Not found");
                    throw new NotFoundException("Product not found");
                }
                if (product.Stock < item.Quantity)
                {
                    Log.Warning("Số lượng sản phẩm không đủ");
                    throw new BussinesErrorException("Số lượng đặt vượt quá số lượng sản phẩm hiện có");
                }
                order.AddOrderItem(product.Id, product.Name, item.Quantity, product.Price);
                product.UpdateStock(item.Quantity);
                await _repoProduct.UpdateAsync(product);
            }

            order.RecalculateAmount();
            if (order.VoucherId.HasValue)
            {
                var voucher = await _voucherRepository.GetByIdAsync(order.VoucherId ?? 0);
                if (voucher is null)
                    throw new NotFoundException("Voucher is not found");

                if (voucher.ExpiryDate < DateTime.UtcNow)
                    throw new BussinesErrorException("Voucher đã hết hạn");
                if (order.TotalAmount < voucher.MinOrder)
                {
                    throw new BussinesErrorException("Đơn hàng không đủ điều kiện để app mã");
                }
                var discountAmount = Voucher.CalculateDiscountVouchers(order.TotalAmount, voucher.Value, DiscountTypes.Percentage);
                order.ApplyDiscount(discountAmount);
                voucher.Use();
                await _voucherRepository.UpdateAsync(voucher);
            }

            order.RecalculateAmount();

            if (command.PaymentMethod.HasValue)
            {
                var method = (PaymentMethod)command.PaymentMethod;
                if (!Enum.IsDefined(typeof(PaymentMethod), method))
                {
                    throw new BussinesErrorException("Invalid Payment method");
                }
                order.SetPaymentMethod(method);
            }

            Log.Information("Start insert Db,");
            await _repository.CreateAsync(order);
            try
            {
                var notification = new CreateNotificationCommand
                {
                    UserId = user.ToString(),
                    Title = "Bạn đã đặt hàng thành công",
                    Message = $"Đơn hàng của bạn với mã {order.Id} đã được tạo thành công và đang chờ xử lý."
                };
                await _mediator.Send(notification, token);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Không thể gửi thông báo SignalR cho user {UserId}", user);
                // Không throw exception ở đây để tránh rollback đơn hàng chỉ vì lỗi thông báo
            }

            return Unit.Value;
        }
    }
}