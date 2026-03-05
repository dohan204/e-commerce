using MediatR;

namespace application.cases.Commands.Categories
{
    public class CreateCategoryCommand : IRequest<Unit>
    {
        public string Name {get; set;} = string.Empty;
        public string? Image {get; set;} = string.Empty;
        // public string 
    }
}