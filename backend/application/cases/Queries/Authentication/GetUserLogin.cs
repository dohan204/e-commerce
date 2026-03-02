using application.cases.Dtos;
using application.interfaces;

namespace application.cases.Queries.Authentication
{
    public class GetUserLoginHandler
    {
        private readonly IAuthRepository _authRepo;
        public GetUserLoginHandler(IAuthRepository authRepo) 
        => _authRepo = authRepo;

        public async Task<ResponseLogin> Handle(GetUserLoginCommand command)
        {
            var result = await _authRepo.LoginRequest(command);
            return result;
        }
    }
}