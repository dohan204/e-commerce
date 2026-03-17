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

            var newImagePath = await fileStorageService.SaveFileAsync(command.ImageUrl, command.FileName);

            // lấy lại đường dẫn cú 
            var oldStringImage = product.Images;
            product.UpdateImage(newImagePath);
            await productRepository.UpdateAsync(product);

            // xóa đi cái ảnh cũ nếu có 
            if(!string.IsNullOrEmpty(oldStringImage))
                await fileStorageService.DeleteFileAsync(oldStringImage);

            // lưu ảnh mới 
            return newImagePath;
        }
    }
}