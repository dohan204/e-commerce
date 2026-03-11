using MediatR;

namespace application.cases.Commands.Users
{
    public class RemoveUserCommand : IRequest<Unit>
    {
        public Guid id {get; set;}
    }
}