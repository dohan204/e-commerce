using MediatR;
using application.interfaces;
using application.cases.Dtos;
namespace application.cases.Queries.Products
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery,IReadOnlyList<ProductViewDto>>
    {
        private readonly IProductRepository _repository;
        public GetProductsHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<ProductViewDto>> Handle(GetProductsQuery query, CancellationToken token)
        {
            var products = await _repository.GetProductsAsync();
            return products;
        }
    }
}