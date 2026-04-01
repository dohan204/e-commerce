using application.interfaces;
using domain.entities;
using domain.interfaces;
using MediatR;

namespace application.cases.Commands.Categories
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Unit>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IFileStorageService fileStorageService;
        public CreateCategoryHandler(ICategoryRepository categoryRepository, IFileStorageService fileStorageService)
        {
            _categoryRepository = categoryRepository;
            this.fileStorageService = fileStorageService;
        }
        public async Task<Unit> Handle(CreateCategoryCommand command, CancellationToken token)
        {

            // create path images
            var imagePath = await fileStorageService.SaveFileAsync(command.Image!, command.FileName!);
            var category = new Category(command.Name, imagePath);
            await _categoryRepository.CreateAsync(category);
            return Unit.Value;
        }
    }
}