using System.Net;
using application.exceptions;
namespace application.exceptions
{
    public class NotFoundException : ExceptionBase
    {
        public NotFoundException(string message) : base(message, HttpStatusCode.NotFound, "NOT_FOUND"){}
    }
}