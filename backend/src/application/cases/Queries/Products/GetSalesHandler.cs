using application.cases.Dtos;
using application.interfaces;
using MediatR;

namespace application.cases.Queries.Products
{
    public sealed class GetSalesHandler : IRequestHandler<GetSales, IEnumerable<ProductViewDto>>
    {
        private readonly IProductRepository productRepository;
        public GetSalesHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductViewDto>> Handle(GetSales sales, CancellationToken token)
        {
            return await productRepository.GetSales();
        }
    } 
}