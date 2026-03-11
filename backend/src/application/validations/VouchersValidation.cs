using application.cases.Commands.Vouchers;
using FluentValidation;

namespace application.validations
{
    public class VouchersValidation : AbstractValidator<CreateVoucherCommand>
    {
        public VouchersValidation()
        {
            RuleFor(e => e.DiscountType)
                .NotEmpty().WithMessage("Kiểu giảm giá không được để trống");

            RuleFor(e => e.Value)
                .NotEmpty().WithMessage("Giá trị giảm không được để trôngs")
                .InclusiveBetween(1, 100).WithMessage("Chỉ trong khoảng từ 1 tới 100");

            RuleFor(e => e.MinOrder)
                .NotEmpty().WithMessage("Phần áp dụng cho đơn hàng này không được để trống")
                .InclusiveBetween(1, 1000).WithMessage("Chỉ áp dụng trong khoảng 1 tới 1000");
            RuleFor(e => e.MaxUsage)
                .NotEmpty().WithMessage("Số lần được sử dụng không được để trống")
                .InclusiveBetween(10, 500).WithMessage("Số lần sử dụng chỉ ở trong khoảng từ 10 tới 500");
        }
    }
}