using application.cases.Dtos;
using application.exceptions;
using application.interfaces;
using MediatR;

namespace application.cases.Queries.Products
{
    public class GetProductByIdHandler : IRequestHandler<GetProductCommand, ProductViewDto>
    {
        private readonly IProductRepository _productRepository;
        public GetProductByIdHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductViewDto> Handle(GetProductCommand command, CancellationToken token)
        {
            var product = await _productRepository.GetProductById(command.Id);
            if(product == null)
                throw new NotFoundException("Không tìm thấy sản phẩm");
            return new ProductViewDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                SalePrice = (decimal)product.SalePrice,
                AvgRating = (decimal)product.AvgRating,
                ImageUrl = product.Images,
            };
        }
    }
}