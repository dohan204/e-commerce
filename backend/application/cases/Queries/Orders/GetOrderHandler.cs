using application.interfaces;
using domain.entities;
using MediatR;

namespace application.cases.Queries.Orders
{
    public class GetOrderHandler : IRequestHandler<GetOrderQuery, Order>
    {
        private readonly IOrderRepository _repository;
        public GetOrderHandler(IOrderRepository orderRepository)
        {
            _repository = orderRepository;
        }

        public async Task<Order> Handle(GetOrderQuery query, CancellationToken token)
        {
            var order = await _repository.GetOrderByIdAsync(query.Id);
            return order;
        }
    }
}