using application.interfaces;
using MediatR;

namespace application.cases.Queries.Products
{
    public class GetProductDataDashboardHandler : IRequestHandler<GetProductDataDashboardQuery, object?>
    {
        private readonly IProductRepository productRepository;
        public GetProductDataDashboardHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<object?> Handle(GetProductDataDashboardQuery query, CancellationToken token)
        {
            return await productRepository.GetFullDashboardStatsAsync();
        }
    }
}