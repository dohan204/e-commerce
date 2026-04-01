using MediatR;
using application.interfaces;
using FluentValidation;
using domain.entities;
using application.exceptions;
using domain.interfaces;
using Serilog;
namespace application.cases.Commands.Product
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Unit>
    {
        private readonly IFileStorageService fileStorageService;
        private readonly IProductRepository _productRepository;
        private readonly IValidator<CreateProductCommand> _validator;
        public CreateProductHandler(IProductRepository productRepository, IValidator<CreateProductCommand> validator, IFileStorageService fileStorageService)
        {
            _productRepository = productRepository;
            _validator = validator;
            this.fileStorageService = fileStorageService;
        }

        public async Task<Unit> Handle(CreateProductCommand command, CancellationToken token)
        {
            var product = await _validator.ValidateAsync(command);
            if(!product.IsValid)
                throw new BussinesErrorException(string.Join(", ", product.Errors.Select(e => e.ErrorMessage)));

            var imageUrl = await fileStorageService.SaveFileAsync(command.ImageUrl!, command.FileName!);
            if(imageUrl == null)
                throw new BussinesErrorException("Failed to save image");

            Log.Information("Create product: {Name} - {Description} - {Price} - {Stock} - {CategoryId} - {ImageUrl}", command.Name, command.Description, command.Price, command.Stock, command.CategoryId, imageUrl);
            var newProduct = new Products(
                command.Name,
                command.Description,
                command.Price,
                command.Stock,
                command.CategoryId,
                imageUrl,
                command.Tag
            );

            await _productRepository.AddAsync(newProduct);
            return Unit.Value;
        }
    }
}