using application.interfaces;
using MediatR;

namespace application.cases.Commands.Reviews
{
    public class CreateReviewCommand : IRequest<Unit>
    {
        public int ProductEntityId {get; set;} 
        public int Ratings {get; set;} 
        public string Comments {get; set;} = string.Empty;
    }
}