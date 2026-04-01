using application.cases.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace application.cases.Queries.Products
{
    public class GetSearchProductQuery : IRequest<IEnumerable<ProductViewDto>>
    {
        [BindRequired]
        public string Search {get; set;} = string.Empty;
    }
}