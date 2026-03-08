using application.exceptions;
using application.interfaces;
using domain.interfaces;
using MediatR;

namespace application.cases.Commands.Product
{
    public class UpdateProductImageHandler : IRequestHandler<UpdateProductImageCommand, string>
    {
        private readonly IProductRepository productRepository;
        private readonly IFileStorageService fileStorageService;
        public UpdateProductImageHandler(IProductRepository productRepository, IFileStorageService fileStorageService)
        {
            this.productRepository = productRepository;
            this.fileStorageService = fileStorageService;
        }

        public async Task<string> Handle(UpdateProductImageCommand command, CancellationToken token)
        {
            var product = await productRepository.GetProductById(command.ProductId);
            if(product is null)
                throw new NotFoundException($"Product with id: {command.ProductId} Not found");

            // xóa đi cái ảnh cũ nếu có 
            if(!string.IsNullOrEmpty(product.Images))
                await fileStorageService.DeleteFileAsync(product.Images);

            // lưu ảnh mới 
            var newImagePath = await fileStorageService.SaveFileAsync(command.ImageUrl, command.FileName);
            product.UpdateImage(newImagePath);
            await productRepository.UpdateAsync(product);
            return newImagePath;
        }
    }
}