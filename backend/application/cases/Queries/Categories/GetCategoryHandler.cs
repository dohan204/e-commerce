using application.cases.Dtos;
using application.exceptions;
using application.interfaces;
using domain.entities;
using MediatR;

namespace application.cases.Queries.Categories
{
    public class GetCategoryHandler : IRequestHandler<GetCategoryQuery, Category>
    {
        private readonly ICategoryRepository _categories;
        public GetCategoryHandler(ICategoryRepository categories)
        {
            _categories = categories;
        }
        public async Task<Category> Handle(GetCategoryQuery command, CancellationToken token)
        {
            var product = await _categories.GetCategoryAsync(command.Id);
            if(product is null)
                throw new NotFoundException($"Category id: {command.Id} not found");
            return product;
        }
    } 
}