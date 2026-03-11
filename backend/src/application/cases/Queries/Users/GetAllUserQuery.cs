using application.cases.Dtos;
using application.exceptions;
using domain.interfaces;

namespace application.cases.Queries.Users
{
    public class GetAllUserQuery
    {
        private readonly IUserRepository _userRepository;
        public GetAllUserQuery(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        } 
        public async Task<IReadOnlyList<UserResponseList>> Handle()
        {
            var users = await _userRepository.GetAllUserAsync();
            
            return users.Select(e => new UserResponseList(
                e.UserId,
                e.Name,
                e.FullName!,
                e.Email,
                e.Created_At,
                e.Updated_At
            )
            ).ToList();
        }
    }
}