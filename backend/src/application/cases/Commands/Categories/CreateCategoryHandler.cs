using application.interfaces;
using domain.entities;
using MediatR;

namespace application.cases.Commands.Categories
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Unit>
    {
        private readonly ICategoryRepository _categoryRepository;
        public CreateCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<Unit> Handle(CreateCategoryCommand command, CancellationToken token)
        {
            var category = new Category(command.Name, command.Image);
            await _categoryRepository.CreateAsync(category);
            return Unit.Value;
        }
    }
}