namespace application.exceptions
{
    public class ForbidenException : ExceptionBase
    {
        public ForbidenException(string message) : base(message, System.Net.HttpStatusCode.Forbidden, "FOR_BIDEN"){}
    }
}