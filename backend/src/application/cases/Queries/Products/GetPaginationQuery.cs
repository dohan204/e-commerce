using application.cases.Dtos;
using application.helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace application.cases.Queries.Products
{
    public class GetPaginationQuery : IRequest<PagedResult<ProductViewDto>>
    {
        // [BindRequired]
        public int Page {get; set;}
        // [BindRequired]
        public int PageSize {get; set;}
        public string? Search {get; set;}
        public int? CategoryId {get; set;}
    }
}