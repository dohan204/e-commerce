using MediatR;
using application.interfaces;
using application.exceptions;
using domain.entities;
namespace application.cases.Commands.Orders
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, Unit>
    {
        private readonly IOrderRepository _orderRepository;
        public UpdateOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Unit> Handle(UpdateOrderCommand command, CancellationToken token)
        {
            var order = await _orderRepository.GetOrderByIdAsync(command.OrderId);
            if(order is null) 
                throw new NotFoundException("Order not found");

            order.Update(command.DiscountAmount, command.ShippingFee, command.ShippingAddress, command.Note);
            
            order.Items.Clear(); // xóa hết các items cũ đi 
            foreach(var item in command.UpdateOrderItem)
            {
                var itemUpdate = OrderItem.Update(item.ProductId, item.Quantity, item.Price);
                order.Items.Add(itemUpdate);
            }

            var totalAmount = order.Items.Sum(x => x.Quantity * x.Price);
            order.ReturnFinal(totalAmount - command.DiscountAmount + command.ShippingFee);

            await _orderRepository.UpdateAsync(order);
            return Unit.Value;
        }
    }
}