using application.exceptions;
using domain.interfaces;
using MediatR;

namespace application.cases.Commands.Users
{
    public class RemoveUserHandler
    {
        private readonly IUserRepository userRepository;
        public RemoveUserHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<Unit> Handle(RemoveUserCommand command, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(command.id);

            if(user is null) 
                throw new NotFoundException("Không tìm thấy user cần xóa");
            
            await userRepository.RemoveUser(user.UserId);

            return Unit.Value;
        }
    }
}