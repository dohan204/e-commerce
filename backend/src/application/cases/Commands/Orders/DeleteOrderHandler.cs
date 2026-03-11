using application.exceptions;
using application.interfaces;
using MediatR;

namespace application.cases.Commands.Orders
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, Unit>
    {
        private readonly IOrderRepository _repository;
        public DeleteOrderHandler(IOrderRepository repository)
        {
            _repository = repository;
        }
        public async Task<Unit> Handle(DeleteOrderCommand command, CancellationToken token)
        {
            var order = await _repository.DeleteAsync(command.Id);
            if(!order)
                throw new NotFoundException($"Không tìm thấy đơn hàng với id: {command.Id}");
            return Unit.Value;
        }
    }
}