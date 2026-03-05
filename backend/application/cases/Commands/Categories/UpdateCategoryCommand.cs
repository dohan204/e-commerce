using System.ComponentModel;
using MediatR;

namespace application.cases.Commands.Categories
{
    public class UpdateCategoryCommand : IRequest<Unit>
    {
        public int Id {get; set;}
        public string Name {get; set;} = string.Empty;
        public string? Image {get; set;} 
        public string? Slug {get; set;}
    }
}