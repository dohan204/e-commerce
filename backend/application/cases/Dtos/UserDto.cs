using System.Dynamic;

namespace application.cases.Dtos
{
    public record UserResponse(Guid id, string name, string email, string role);
    public record UserResponseList(Guid id, string name, string fullName, string email, DateTime? created, DateTime? updated);
    public class UserCreateDto
    {
        public string UserName {get; set;} = string.Empty;
        public string Email {get; set;} = string.Empty;
        public string Password {get; set;} = string.Empty;
    }
}