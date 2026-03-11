using System.Net;

namespace application.exceptions
{
    public class UnauthorizeException : ExceptionBase
    {
        public UnauthorizeException(string message) : base(message, HttpStatusCode.Unauthorized, "UNAUTHORIZED") {}
    }
}