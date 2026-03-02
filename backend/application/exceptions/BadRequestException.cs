using System.Net;

namespace application.exceptions
{
    public class BadRequestException : ExceptionBase
    {
        public BadRequestException(string message) : base(message ,HttpStatusCode.BadRequest,"BAD_REQUEST"){}
    }
}