using application.cases.Dtos;
using MediatR;

namespace application.cases.Queries.Categories
{
    public class GetCategoriesQuery : IRequest<IReadOnlyList<CategoryViewDto>>
    {
        
    }
}