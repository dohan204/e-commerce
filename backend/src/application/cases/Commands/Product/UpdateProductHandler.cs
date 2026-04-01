using application.exceptions;
using application.interfaces;
using domain.entities;
using domain.interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog;

namespace application.cases.Commands.Product
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly IProductRepository productRepository;
        private readonly IFileStorageService fileStorageService;
        public UpdateProductHandler(IProductRepository productRepository, IFileStorageService fileStorageService)
        {
            this.productRepository = productRepository;
            this.fileStorageService = fileStorageService;
        }

        public async Task<Unit> Handle(UpdateProductCommand command, CancellationToken token)
        {
            var product = await productRepository.GetProductById(command.Id);
            // var product = await 
            if (product is null)
            {
                throw new NotFoundException("Product Not Found");
            }
            var oldImage = product.Images;
            string newImagePath = oldImage;
            if (command.ImageUrl is not null)
                newImagePath = await fileStorageService.SaveFileAsync(command.ImageUrl, command.FileName);


            product.Update(
                string.IsNullOrEmpty(command.Name) ? product.Name : command.Name,
                string.IsNullOrEmpty(command.Description) ? product.Description : command.Description,
                command.Price ?? product.Price,
                command.Stock ?? product.Stock,
                command.Sold ?? product.Sold,
                command.SalePrice ?? product.SalePrice,
                command.CategoryId ?? product.CategoryId,
                newImagePath
            );
            Log.Information($"Product Update: {product.Id} - {product.Name} - {product.Price} - {product.Stock} - {product.Sold} - {product.SalePrice} - {product.CategoryId} - {product.Images}");
            await productRepository.UpdateAsync(product);
            if (command.ImageUrl is not null && string.IsNullOrEmpty(oldImage))
                await fileStorageService.DeleteFileAsync(oldImage);

            return Unit.Value;

        }
    }
}