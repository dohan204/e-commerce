using System.Data;
using application.cases.Commands.Addresses;
using FluentValidation;

namespace application.validations
{
    public class AddressValidation : AbstractValidator<CreateAddressCommand>
    {
        public AddressValidation()
        {
            RuleFor(e => e.UserId)
                .NotNull().WithMessage("Mã người dùng không được null")
                .NotEmpty().WithMessage("Mã người dùng không được bỏ trống");

            RuleFor(e => e.Province)
                .NotEmpty().WithMessage("Tên thành phố không được để trống");

            RuleFor(e => e.District)
                .NotEmpty().WithMessage("Tên xã phường kh được để trống");
            RuleFor(e => e.Ward)
                .NotEmpty().WithMessage("Tên thôn bản kh đucợ để trống");
        }
    }
}