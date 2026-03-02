namespace application.cases.Dtos
{
    public record ResponseLogin(string token);
    public class LoginRequest
    {
        public string Email {get; set;} = string.Empty;
        public string Password {get; set;} = string.Empty;
    }
}