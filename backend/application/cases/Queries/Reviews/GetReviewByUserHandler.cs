// using application.interfaces;
// using domain.entities;
// using MediatR;

// namespace application.cases.Queries.Reviews
// {
//     public class GetReviewByUserHandler : IRequestHandler<GetReviewByUserQuery, Review>
//     {
//         private readonly IReviewRepository _reviewRepository;
//         private readonly ICurrentUser _user;
//         public GetReviewByUserHandler(IReviewRepository reviewRepository, ICurrentUser user)
//         {
//             _reviewRepository = reviewRepository;
//             _user = user;
//         }

//         public async Task<Review> Handle(GetReviewByUserQuery query, CancellationToken token)
//         {
            
//         }
//     }
// }