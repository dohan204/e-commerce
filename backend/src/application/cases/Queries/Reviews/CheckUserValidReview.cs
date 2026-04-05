using MediatR;

namespace application.cases.Queries.Reviews
{
    public class CheckUserValidReview : IRequest<bool>
    {
        public int ProductId {get; set;}
    }
}