namespace application.cases.Queries.Authentication
{
    public record GetUserLoginCommand(string username, string password);
}