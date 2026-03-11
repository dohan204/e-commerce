using application.exceptions;
using application.interfaces;
using domain.entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace application.cases.Commands.Product
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly IProductRepository productRepository;
        public UpdateProductHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<Unit> Handle(UpdateProductCommand command, CancellationToken token) 
        {
            var product = await productRepository.GetProductById(command.Id);
            // var product = await 
            if(product is null)
            {
                throw new NotFoundException("Product Not Found");
            }
           
            product.Update(
                command.ProductsUpdate.Name,
                command.ProductsUpdate.Description,
                command.ProductsUpdate.Price,
                command.ProductsUpdate.Stock,
                command.ProductsUpdate.Sold,
                command.ProductsUpdate.SalePrice,
                command.ProductsUpdate.CategoryId,
                command.ProductsUpdate.ImageUrl,
                command.ProductsUpdate.ReviewCount,
                command.ProductsUpdate.AvgRating
            );

            await productRepository.UpdateAsync(product);
            return Unit.Value;

        }
    }
}