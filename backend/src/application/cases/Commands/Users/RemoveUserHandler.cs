using application.exceptions;
using domain.interfaces;
using MediatR;

namespace application.cases.Commands.Users
{
    public class RemoveUserHandler : IRequestHandler<RemoveUserCommand,bool>
    {
        private readonly IUserRepository userRepository;
        public RemoveUserHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<bool> Handle(RemoveUserCommand command, CancellationToken cancellationToken)
        {
            var isDelete = await userRepository.RemoveUser(command.id);
            if(!isDelete)
                throw new NotFoundException("Không tìm thấy Người dùng");
            return true;
        }
    }
}