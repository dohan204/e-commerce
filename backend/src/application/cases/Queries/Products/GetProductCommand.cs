using application.cases.Dtos;
using MediatR;

namespace application.cases.Queries.Products
{
    public class GetProductCommand : IRequest<ProductViewDto>
    {
        public int Id { get; set; }
    }
}