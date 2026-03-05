using MediatR;

namespace application.cases.Commands.Categories
{
    public class DeleteCategoryCommand : IRequest<bool>
    {
        public int CategoryId { get; set; }
    } 
    
}