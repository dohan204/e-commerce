using application.interfaces;
using MediatR;

namespace application.cases.Queries.Products
{
    public class GetTopSaleProductQuery : IRequest<IEnumerable<TopProductSale>> {};
}