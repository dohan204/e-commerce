using MediatR;
using application.interfaces;
using FluentValidation;
using domain.entities;
using application.exceptions;
namespace application.cases.Commands.Product
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly IValidator<CreateProductCommand> _validator;
        public CreateProductHandler(IProductRepository productRepository, IValidator<CreateProductCommand> validator)
        {
            _productRepository = productRepository;
            _validator = validator;
        }

        public async Task<Unit> Handle(CreateProductCommand command, CancellationToken token)
        {
            var product = await _validator.ValidateAsync(command);
            if(!product.IsValid)
                throw new BussinesErrorException(string.Join(", ", product.Errors.Select(e => e.ErrorMessage)));


            var newProduct = new Products(
                command.Name,
                command.Description,
                command.Price,
                command.Stock,
                command.CategoryId
            );

            await _productRepository.AddAsync(newProduct);
            return Unit.Value;
        }
    }
}