using application.cases.Queries.Users;
using MediatR;

namespace application.cases.Commands.Product
{
    public class CreateProductCommand : IRequest<Unit>
    {
        public string Name {get; set;} = string.Empty;
        public string Description {get; set;} = string.Empty;
        public decimal Price {get; set;} = decimal.Zero;
        public int Stock {get; set; }
        public int CategoryId {get; set;}
        public string Tag {get; set;} = string.Empty;
        public Stream? ImageUrl {get; set;}
        public string? FileName {get; set;} 
    }
}