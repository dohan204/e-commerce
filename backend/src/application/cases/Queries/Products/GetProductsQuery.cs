using application.cases.Dtos;
using MediatR;

namespace application.cases.Queries.Products
{
    public class GetProductsQuery : IRequest<IEnumerable<ProductViewDto>> {}
}