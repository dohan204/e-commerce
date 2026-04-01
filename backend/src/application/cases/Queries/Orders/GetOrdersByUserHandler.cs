using MediatR;
using domain.entities;
using application.interfaces;
namespace application.cases.Queries.Orders
{
    public class GetOrdersByUserHandler : IRequestHandler<GetOrdersByUserQuery, IEnumerable<Order>>
    {
        private readonly IOrderRepository _repository;
        public GetOrdersByUserHandler(IOrderRepository orderRepository)
        {
            _repository = orderRepository;
        }

        public async Task<IEnumerable<Order>> Handle(GetOrdersByUserQuery query, CancellationToken token)
        {
            var orders = await _repository.GetOrdersByUserIdAsync(query.UserId);
            return orders;
        }
    }
}