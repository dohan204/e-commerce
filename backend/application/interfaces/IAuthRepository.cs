using application.cases.Dtos;
using application.cases.Queries.Authentication;
// using application.
namespace application.interfaces
{
    public interface IAuthRepository
    {
        Task<ResponseLogin> LoginRequest(GetUserLoginCommand loginRequest);
        Task<bool> ConfirmEmail(string userId, string token);
        // Task GenerateTokenConfirmEmailAsync(AppUser user);
    }
}