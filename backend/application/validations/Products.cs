using application.cases.Commands.Product;
using FluentValidation;

namespace application.validations
{
    public class ProductCreateValidation : AbstractValidator<CreateProductCommand>
    {
        public ProductCreateValidation()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Tên sản phẩm là bắt buộc")
                .NotNull().WithMessage("Tên sản phẩm là bắt buộc")
                .MaximumLength(400).WithMessage("Tên sản phẩm không được vượt quá 400 ký tự");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Vui Lòng nhập mô tả sản phẩm");

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("Giá sản phẩm không được để trống")
                .InclusiveBetween(1000m, 999999999999m).WithMessage("Giá sản phẩm chỉ trong khoảng 1000 tới 999..")
                .PrecisionScale(15, 2, true).WithMessage("Giá sản phẩm theo chuẩn 15.2");

            RuleFor(p => p.Stock)
                .NotEmpty().WithMessage("Số lượng không được để trống")
                .GreaterThan(0).WithMessage("Số lượng phải lớn hơn 0")
                .InclusiveBetween(1, 1000).WithMessage("Số lượng sản phẩm chỉ trong khoản 1 tới 1000");

            RuleFor(p => p.CategoryId)
                .NotEmpty().WithMessage("Loại hàng không được để trống")
                .GreaterThan(0).WithMessage("Mã loại hàng không được nhỏ hơn 0");      
        }
    }
}