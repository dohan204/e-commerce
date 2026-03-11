using MediatR;

namespace application.cases.Commands.Reviews
{
    public class DeleteReviewCommand : IRequest<bool>
    {
        public int ReviewId {get; set;}
    }
}