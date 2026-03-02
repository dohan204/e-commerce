namespace application.exceptions
{
    public class ConflicException : ExceptionBase
    {
        public ConflicException(string message) : base(message, System.Net.HttpStatusCode.Conflict, "CONFLIC"){}
    }
}