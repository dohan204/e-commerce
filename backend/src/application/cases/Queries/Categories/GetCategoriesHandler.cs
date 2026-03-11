using application.cases.Dtos;
using application.interfaces;
using MediatR;

namespace application.cases.Queries.Categories
{
    public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, IReadOnlyList<CategoryViewDto>>
    {
        private readonly ICategoryRepository _category;
        public GetCategoriesHandler(ICategoryRepository category)
        {
            _category = category;
        }
        public async Task<IReadOnlyList<CategoryViewDto>> Handle(GetCategoriesQuery query, CancellationToken token)
        {
            var categories = await _category.GetAllCategoriesAsync();
            return categories;
        }
    }
}