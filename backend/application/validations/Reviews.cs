using application.cases.Commands.Reviews;
using FluentValidation;

namespace application.validations
{
    public class ReviewValidation : AbstractValidator<CreateReviewCommand>
    {
        
        public ReviewValidation()
        {
            RuleFor(e => e.Comments)
                .NotEmpty().WithMessage("Comments is not empty.")
                .Length(10, 200).WithMessage("Đánh giá không được vượt quá 200 ký tự");

            RuleFor(e => e.Ratings)
                .NotEmpty().WithMessage("Đánh giá không được để trống");
        }
    }
}