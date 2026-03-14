using System.Reflection;
using domain.enums;

namespace application.cases.Dtos
{
    public class ProductUpdateDto
    {
        public int Id { get; set; }
        public string Name {get; set;} = string.Empty;
        public string? Description {get; set;} = string.Empty;
        public decimal? Price {get; set;}
        public decimal? SalePrice {get; set;}
        public int? Stock {get; set;}
        public int? Sold {get; set;}
        public int? CategoryId {get; set;}
        public string? ImageUrl {get; set;}
        public decimal? AvgRating {get; set;}
        public int? ReviewCount {get; set;}
        public StatusProduct? StatusProduct{get; set;}
    }
    public class ProductViewDto
    {
        public int Id {get;set; } 
        public string Name {get; set;} = string.Empty;
        public string? Description {get; set;} = string.Empty;
        public int? Stock {get; set;}
        public decimal Price {get; set;} 
        public decimal? SalePrice {get; set;}
        public string? ImageUrl {get; set;}
        public decimal? AvgRating {get; set;}
    }
}