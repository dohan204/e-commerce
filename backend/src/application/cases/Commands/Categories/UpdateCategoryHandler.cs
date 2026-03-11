using application.exceptions;
using application.interfaces;
using domain.entities;
using MediatR;

namespace application.cases.Commands.Categories
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Unit>
    {
        private readonly ICategoryRepository _categoryRepository;
        public UpdateCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand command, CancellationToken token)
        {
            var category = await _categoryRepository.GetCategoryAsync(command.Id);
            if(category is null)
                throw new NotFoundException("Category not found");
            category.Update(command.Name, command.Image);
            
            await _categoryRepository.UpdateAsync(category);
            return Unit.Value;
        }
    }
}