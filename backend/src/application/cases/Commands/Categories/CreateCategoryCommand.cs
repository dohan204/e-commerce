using MediatR;

namespace application.cases.Commands.Categories
{
    public class CreateCategoryCommand : IRequest<Unit>
    {
        public string Name {get; set;} = string.Empty;
        public Stream? Image {get; set;}
        public string? FileName { get; set; }
        // public string 
    }
}