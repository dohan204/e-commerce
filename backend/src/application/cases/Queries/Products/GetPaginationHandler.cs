using application.cases.Dtos;
using application.helpers;
using application.interfaces;
using MediatR;

namespace application.cases.Queries.Products
{
    public class GetPaginationHandler: IRequestHandler<GetPaginationQuery, PagedResult<ProductViewDto>>
    {
        private readonly IProductRepository _productRepository;
        public GetPaginationHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<PagedResult<ProductViewDto>> Handle(GetPaginationQuery query, CancellationToken token)
        {
            if(query.Page == 0 || query.PageSize == 0)
            {
                throw new ArgumentNullException("Không có dữ liệu để truy vấn");                
            }

            var result = await _productRepository.PaginationProduct(query.Page, query.PageSize, query.Search, query.CategoryId);
            return result;
        }
    }
}