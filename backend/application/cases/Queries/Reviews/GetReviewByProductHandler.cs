using application.exceptions;
using application.interfaces;
using domain.entities;
using FluentValidation;
using MediatR;

namespace application.cases.Queries.Reviews
{
    public class GetReviewByProductHandler : IRequestHandler<GetReviewByProductQuery, IReadOnlyList<Review>>
    {
        private readonly IValidator<GetReviewByProductQuery> _validator;
        private readonly IReviewRepository _reviewRepository;
        public GetReviewByProductHandler(IReviewRepository reviewRepository, IValidator<GetReviewByProductQuery> validator)
        {
            _reviewRepository = reviewRepository;
            _validator = validator;
        }
        public async Task<IReadOnlyList<Review>> Handle(GetReviewByProductQuery query, CancellationToken token)
        {
            // check id input 
            var id = await _validator.ValidateAsync(query);

            if(!id.IsValid)
               throw new BadRequestException(string.Join(",", id.Errors.Select(e => e.ErrorMessage)));


            var reviews = await _reviewRepository.GetReviewsAllByProduct(query.ProductId);
            return reviews;
        }
    }
}