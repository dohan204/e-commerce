using MediatR;

namespace application.cases.Commands.Addresses
{
    public class CreateAddressCommand : IRequest<Unit>
    {
        public Guid UserId {get; set;}
        public string Province {get; set;} = string.Empty;
        public string District {get; set;} = string.Empty;
        public string Ward {get; set;} = string.Empty;
        public string? Details {get; set; } = string.Empty;
        public string Phone {get; set;} = string.Empty;
    }
}