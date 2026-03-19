using application.interfaces;
using MediatR;

namespace application.cases.Queries.Orders
{
    public class GetTotalRevenceHandler : IRequestHandler<GetTotalRevenueQuery, IEnumerable<RevenueDto>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetTotalRevenceHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<IEnumerable<RevenueDto>> Handle(GetTotalRevenueQuery query, CancellationToken token)
        {
            return await _orderRepository.GetDecimalAsync();
        }
    }
}