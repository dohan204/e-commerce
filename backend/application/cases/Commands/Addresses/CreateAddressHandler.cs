using application.cases.Commands.Addresses;
using application.exceptions;
using application.interfaces;
using domain.entities;
using FluentValidation;
using MediatR;

namespace application.cases.Commands.Addresses
{
    public class CreateAddressHandler : IRequestHandler<CreateAddressCommand, Unit>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IValidator<CreateAddressCommand> validator;
        private readonly ICurrentUser currentUser;
        public CreateAddressHandler(IAddressRepository addressRepository, 
        IValidator<CreateAddressCommand> validator, ICurrentUser currentUser)
        {
            _addressRepository = addressRepository;
            this.validator = validator;
            this.currentUser = currentUser;
        }
        public async Task<Unit> Handle(CreateAddressCommand command, CancellationToken cancellation)
        {
            if(Guid.TryParse(currentUser.UserId, out var userId))
                throw new UnauthorizeException("User invalie");
            var inputCreatedValidate = await validator.ValidateAsync(command);
            if(!inputCreatedValidate.IsValid)
                throw new BadRequestException(string.Join(",", inputCreatedValidate.Errors.Select(e => e.ErrorMessage)));

            var address = Address.Create(
                userId,
                command.Province,
                command.District,
                command.Ward,
                command.Phone
            );

            await _addressRepository.CreateAsync(address);
            return Unit.Value;
        }
    }
}