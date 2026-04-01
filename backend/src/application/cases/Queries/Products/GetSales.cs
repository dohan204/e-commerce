using application.cases.Dtos;
using MediatR;

namespace application.cases.Queries.Products
{
    public class GetSales : IRequest<IEnumerable<ProductViewDto>> {}
}