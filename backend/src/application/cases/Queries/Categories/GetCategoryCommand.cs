using application.cases.Dtos;
using domain.entities;
using MediatR;

namespace application.cases.Queries.Categories
{
    public class GetCategoryQuery : IRequest<Category>
    {
        public int Id { get; set; }
    }
}