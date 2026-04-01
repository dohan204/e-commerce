using application.cases.Commands.Notifications;
using application.exceptions;
using application.interfaces;
using MediatR;
using Serilog;

namespace application.cases.Commands.Orders
{
    public class UpdateStatusOrderHandler : IRequestHandler<UpdateStatusOrderCommand, Unit>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IVoucherRepository _voucherRepository;
        private readonly IMediator _mediator;
        private readonly ICurrentUser currentUser;
        public UpdateStatusOrderHandler(IOrderRepository orderRepository, IProductRepository productRepository,
        IVoucherRepository voucherRepository, IMediator mediator, ICurrentUser currentUser)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _voucherRepository = voucherRepository;
            _mediator = mediator;
            this.currentUser = currentUser;
        }

        public async Task<Unit> Handle(UpdateStatusOrderCommand command, CancellationToken token)
        {
            if (!Guid.TryParse(currentUser.UserId, out var user))
            {
                throw new UnauthorizeException("Người dùng không hợp lệ.");
            }
            var order = await _orderRepository.GetOrderByIdAsync(command.OrderId);
            if (order is null)
            {
                Log.Warning($"Không tìm thấy đơn hàng với Id: {command.OrderId}");
                throw new NotFoundException("Không tìm thấy đơn hàng");
            }

            order.Cancel();
            var voucher = await _voucherRepository.GetByIdAsync(order.VoucherId ?? 0);
            if (voucher is not null)
            {
                voucher.Restore();
                await _voucherRepository.UpdateAsync(voucher);
            }
            // lấy ra sản phẩm trong đơn hàng để restovef 
            foreach (var orderitem in order.Items)
            {
                var product = await _productRepository.GetProductById(orderitem.ProductId);
                if (product is null)
                    throw new NotFoundException("Not found product is exist order");
                product.RestoreStock(orderitem.Quantity);
                await _productRepository.UpdateAsync(product);
            }
            await _orderRepository.UpdateAsync(order);
            Log.Information("Cập nhật thành công.");

            try
            {
                string message = HelperStatusOrder.GetStringStatus(3);

                await _mediator.Send(new CreateNotificationCommand
                {
                    UserId = user.ToString(),
                    Title = "Hủy đơn hàng",
                    Message = message
                });
                Log.Information("Gửi thông báo thành công");
            }
            catch (Exception)
            {
                throw;
            }
            return Unit.Value;
        }
    }
}