using System.Reflection;
using domain.enums;
using Microsoft.AspNetCore.Http;

namespace application.cases.Dtos
{
    public class ProductUpdateDto
    {
        
    }
    public class ProductViewDto
    {
        public int Id {get;set; } 
        public string Name {get; set;} = string.Empty;
        public string? Description {get; set;} = string.Empty;
        public int Stock {get; set;}
        public int? Sold {get; set;}
        public decimal Price {get; set;} 
        public decimal? SalePrice {get; set;}
        public string? ImageUrl {get; set;}
        public int? CategoryId {get; set;}
        public decimal? AvgRating {get; set;}
        public ICollection<ReviewDto> Reviews {get; set;}
        public int? ReviewCount {get; set;}
    }

    public class ReviewDto
    {
        public int Id {get; set;}
        public Guid UserId {get; set;}
        public int ProductEntityId {get; set;}
        public int Rating {get; set;}
        public string Comment {get; set;}
    }
}