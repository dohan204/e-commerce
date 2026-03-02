using System.Net;

namespace application.exceptions
{
    public class ExceptionBase : Exception
    {
        public HttpStatusCode status { get; set; }
        public string ErrorMessage {get; set;} = string.Empty; 
        public ExceptionBase(string message,HttpStatusCode statusCode, string errormessage ) : base(message)
        {
            status = statusCode;
            ErrorMessage = message;
        }
    }
}