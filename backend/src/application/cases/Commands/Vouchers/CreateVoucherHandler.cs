using application.exceptions;
using application.interfaces;
using FluentValidation;
using MediatR;
using domain.entities;
using Serilog;

namespace application.cases.Commands.Vouchers
{
    public class CreateVoucherHandler : IRequestHandler<CreateVoucherCommand, int>
    {
        private readonly IValidator<CreateVoucherCommand> _validator;
        private readonly IVoucherRepository _repository;
        public CreateVoucherHandler(IValidator<CreateVoucherCommand> validator, IVoucherRepository voucherRepository)
        {
            _validator = validator;
            _repository = voucherRepository;
        }

        public async Task<int> Handle(CreateVoucherCommand command, CancellationToken token)
        {
            Log.Information("thực hiện validate đầu vào");
            var valid = await _validator.ValidateAsync(command);
            if(!valid.IsValid) 
                throw new BussinesErrorException(string.Join(",", valid.Errors.Select(e => e.ErrorMessage)));
            Log.Information("Validate thành công, tiến hành tạo voucher");
            var voucher = Voucher.Create(command.DiscountType, command.Value, command.MinOrder, command.MaxUsage, command.ExpiryDate);
            Log.Information("Gọi tới repository thực hiện insert");
            await _repository.CreateAsync(voucher);
            Log.Information("Tạo voucher thành công");
            return voucher.Id;
        }
    }
}