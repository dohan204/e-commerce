using application.cases.Dtos;
using domain.entities;
using MediatR;

namespace application.cases.Commands.Product
{
    public class UpdateProductCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public ProductUpdateDto ProductsUpdate{ get; set; }
    }
}