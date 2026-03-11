using application.cases.Dtos;
using application.interfaces;

namespace application.cases.Commands.Authentication
{
    public class EmailConfirmHanler
    {
        private readonly IAuthRepository _authRepository;
        public EmailConfirmHanler(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        public async Task Handle(EmailConfirmCommand command)
        {
            await _authRepository.ConfirmEmail(command.userId, command.token);
        }
    }
}