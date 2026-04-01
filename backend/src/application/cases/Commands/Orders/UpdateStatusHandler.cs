using application.cases.Commands.Notifications;
using application.exceptions;
using application.interfaces;
using application.services;
using domain.entities;
using MediatR;
using Serilog;

namespace application.cases.Commands.Orders
{
    public class UpdateStatusHandler : IRequestHandler<UpdateStatusCommand, Unit>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMediator _mediator;
        public UpdateStatusHandler(IOrderRepository orderRepository, IMediator mediator)
        {
            this.orderRepository = orderRepository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(UpdateStatusCommand command, CancellationToken token)
        {
            var order = await orderRepository.GetOrderByIdAsync(command.orderId);
            if (order is null)
            {
                Log.Warning($"Không tìm thấy đơn hàng với Id: {command.orderId}");
                throw new NotFoundException("Không tìm thấy đơn hàng");
            }

            if (!Enum.IsDefined(typeof(StatusOrder), command.Status))
            {
                Log.Warning($"Trạng thái cập nhật không hợp lệ: {command.Status}");
                throw new BadRequestException("Trạng thái không hợp lệ!");
            }

            order.UpdateStatus((StatusOrder)command.Status);
            await orderRepository.UpdateAsync(order);
            Log.Information($"Cập nhật thành công Trạng thái đơn hàng: ${command.orderId}");

            try
            {

                string messag = HelperStatusOrder.GetStringStatus(command.Status);
                // Chuyển Enum Status thành text để hiển thị cho thân thiện
                await _mediator.Send(new CreateNotificationCommand
                {
                    UserId = order.UserId.ToString(),
                    Title = "Cập nhật trạng thái đơn hàng",
                    Message = messag
                });
            }
            catch (Exception ex)
            {
                // Log lỗi nhưng không làm fail request cập nhật trạng thái
                Log.Error(ex, "Lỗi gửi SignalR khi cập nhật trạng thái đơn hàng {OrderId}", order.Id);
            }
            return Unit.Value;
        }
        
    }
}