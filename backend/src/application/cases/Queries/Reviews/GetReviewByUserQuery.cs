using domain.entities;
using MediatR;

namespace application.cases.Queries.Reviews
{
    public class GetReviewByUserQuery : IRequest<Review>
    {
        public Guid UserId {get; set;}
    }
}