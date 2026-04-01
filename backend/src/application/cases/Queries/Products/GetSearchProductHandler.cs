using application.cases.Dtos;
using application.exceptions;
using application.interfaces;
using MediatR;

namespace application.cases.Queries.Products
{
    public class GetSearchProductHandler : IRequestHandler<GetSearchProductQuery, IEnumerable<ProductViewDto>>
    {
        private readonly IProductRepository _productRepository;
        public GetSearchProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IEnumerable<ProductViewDto>> Handle(GetSearchProductQuery query, CancellationToken token)
        {
            var product = await _productRepository.SearchProductAsync(query.Search);
            return product;
        }
    }
}