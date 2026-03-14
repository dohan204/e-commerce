using application.exceptions;
using application.interfaces;
using MediatR;

namespace application.cases.Commands.Orders
{
    public class PaymentHandler : IRequestHandler<PaymentCommand, Unit>
    {
        private readonly IOrderRepository orderRepository;
        public PaymentHandler(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<Unit> Handle(PaymentCommand command, CancellationToken token)
        {
            var order = await orderRepository.GetOrderByIdAsync(command.OrderID);

            if(order is null)
                throw new NotFoundException("Không tìm thấy đơn hàng");
            // switch()
            await order.HandlePayment();
            await orderRepository.UpdateAsync(order);
            return Unit.Value;
        }
    }
}