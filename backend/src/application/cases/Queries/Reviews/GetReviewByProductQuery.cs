using MediatR;
using domain.entities;
using FluentValidation;
namespace application.cases.Queries.Reviews
{
    public class GetReviewByProductQuery : IRequest<IReadOnlyList<Review>>
    {
        public int ProductId {get; set;} 
    }
    public class GetReviewValiation : AbstractValidator<GetReviewByProductQuery>
    {
        public GetReviewValiation()
        {
            RuleFor(e => e.ProductId)
                .NotNull().WithMessage("Mã sản phẩm không được để trống")
                .NotEmpty().WithMessage("Mã sản phẩm không được để trống");
        }
    }
}