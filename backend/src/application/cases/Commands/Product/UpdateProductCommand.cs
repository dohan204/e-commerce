using application.cases.Dtos;
using domain.entities;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
// using domain.entities;
namespace application.cases.Commands.Product
{
    public class UpdateProductCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name {get; set;} = string.Empty;
        public string? Description {get; set;} = string.Empty;
        public decimal? Price {get; set;}
        public decimal? SalePrice {get; set;}
        public int? Stock {get; set;}
        public int? Sold {get; set;}
        public int? CategoryId {get; set;}
        [BindNever]
        public Stream? ImageUrl {get; set;}
        public string? FileName {get; set;}
        public decimal? AvgRating {get; set;}
        public int? ReviewCount {get; set;}
    }
}