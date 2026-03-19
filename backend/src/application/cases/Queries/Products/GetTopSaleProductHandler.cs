using application.interfaces;
using MediatR;

namespace application.cases.Queries.Products
{
    public class GetTopSaleProductHandler : IRequestHandler<GetTopSaleProductQuery, IEnumerable<TopProductSale>>
    {
        private readonly IProductRepository _productRepository;
        public GetTopSaleProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<TopProductSale>> Handle(GetTopSaleProductQuery query, CancellationToken token)
        {
            return await _productRepository.GetTopProductSalesAsync();
        }
    }
}