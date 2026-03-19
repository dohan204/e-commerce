using application.exceptions;
using application.interfaces;
using domain.entities;
using MediatR;
using Serilog;

namespace application.cases.Commands.Orders
{
    public class UpdateStatusHandler : IRequestHandler<UpdateStatusCommand, Unit>
    {
        private readonly IOrderRepository orderRepository;
        public UpdateStatusHandler(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<Unit> Handle(UpdateStatusCommand command, CancellationToken token)
        {
            var order = await orderRepository.GetOrderByIdAsync(command.orderId);
            if(order is null)
            {
                Log.Warning($"Không tìm thấy đơn hàng với Id: {command.orderId}");
                throw new NotFoundException("Không tìm thấy đơn hàng");
            }

            if(!Enum.IsDefined(typeof(StatusOrder), command.Status))
            {
                Log.Warning($"Trạng thái cập nhật không hợp lệ: {command.Status}");
                throw new BadRequestException("Trạng thái không hợp lệ!");
            }

            order.UpdateStatus((StatusOrder)command.Status);
            await orderRepository.UpdateAsync(order);
            Log.Information($"Cập nhật thành công Trạng thái đơn hàng: ${command.orderId}");
            return Unit.Value;
        }
    }
}