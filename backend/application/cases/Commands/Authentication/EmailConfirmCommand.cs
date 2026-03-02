namespace application.cases.Commands.Authentication
{
    public record EmailConfirmCommand(string userId, string token);
}