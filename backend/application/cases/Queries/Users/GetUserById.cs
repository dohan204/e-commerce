using application.cases.Dtos;
using domain.entities;
using domain.interfaces;

namespace application.cases.Queries.Users
{
    public class HandlerGetUserById
    {
        private readonly IUserRepository _userRepository;
        public HandlerGetUserById(IUserRepository userRepository) 
        => _userRepository = userRepository;

        public async Task<UserResponse?> Handle(Guid userId)
        {
            var user =  await _userRepository.GetByIdAsync(userId);
            if (user is null) 
                throw new ApplicationException("User Not found");
            return new UserResponse
            (

                user.UserId,
                user.Name,
                user.Email,
                user.Role.ToString()
            );
        }
    }
}