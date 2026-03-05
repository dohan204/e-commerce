using application.interfaces;
using MediatR;

namespace application.cases.Commands.Categories
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly ICategoryRepository categoryRepository;
        public DeleteCategoryHandler(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<bool> Handle(DeleteCategoryCommand command, CancellationToken token)
        {
                await categoryRepository.DeleteAsync(command.CategoryId);
                return true;  
        }

    }
}