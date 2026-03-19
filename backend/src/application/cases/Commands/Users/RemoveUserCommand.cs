using MediatR;

namespace application.cases.Commands.Users
{
    public class RemoveUserCommand : IRequest<bool>
    {
        public Guid id {get; set;}
    }
}