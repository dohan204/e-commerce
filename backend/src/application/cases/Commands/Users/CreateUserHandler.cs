using domain.interfaces;
using domain.entities;

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
            var user = new User(command.Username, command.Email, command.Password);
            
            await _repository.CreatedAsync(user);
        }
    }
}