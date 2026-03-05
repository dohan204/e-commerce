using application.interfaces;
using domain.entities;
using MediatR;

namespace application.cases.Queries.Orders
{
    public class GetOrdersHandler : IRequestHandler<GetOrdersQuery, IReadOnlyList<Order>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetOrdersHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IReadOnlyList<Order>> Handle(GetOrdersQuery query, CancellationToken token) 
        {
            var orders = await _orderRepository.GetAll();
            return orders;
        }
    }
}