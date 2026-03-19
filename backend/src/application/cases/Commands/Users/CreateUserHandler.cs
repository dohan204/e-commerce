using domain.interfaces;
using domain.entities;
using Serilog;
using application.exceptions;

namespace application.cases.Commands.Users
{
    public class CreateUserHandler
    {
        private readonly IUserRepository _repository; 
        public CreateUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateUserCommand command)
        {
            if(await _repository.EmailExists(command.Email))
            {
                Log.Warning("Email ddax ton tai");
                throw new ConflicException("Email Đã tồn tại không thể đăng ký");
            }
            var user = new User(command.Username, command.Email, command.Password);
            
            await _repository.CreatedAsync(user);
        }
    }
}