using application.exceptions;
using application.interfaces;
using MediatR;
using Serilog;

namespace application.cases.Commands.Orders
{
    public class UpdateStatusOrderHandler: IRequestHandler<UpdateStatusOrderCommand, Unit>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        public UpdateStatusOrderHandler(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(UpdateStatusOrderCommand command, CancellationToken token)
        {
            var order = await _orderRepository.GetOrderByIdAsync(command.OrderId);
            if(order is null)
            {
                Log.Warning($"Không tìm thấy đơn hàng với Id: {command.OrderId}");
                throw new NotFoundException("Không tìm thấy đơn hàng");
            }

            order.Cancel();

            // lấy ra sản phẩm trong đơn hàng để restovef 
            foreach(var orderitem in order.Items)
            {
                var product = await _productRepository.GetProductById(orderitem.ProductId);
                if(product is null)
                    throw new NotFoundException("Not found product is exist order");
                product.RestoreStock(orderitem.Quantity);
                await _productRepository.UpdateAsync(product);
            }
            await _orderRepository.UpdateAsync(order);
            Log.Information("Cập nhật thành công.");
            return Unit.Value;
        }
    }
}