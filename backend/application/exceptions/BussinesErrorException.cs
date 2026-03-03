namespace application.exceptions
{
    public class BussinesErrorException : ExceptionBase
    {
        public BussinesErrorException(string message) : base(message, System.Net.HttpStatusCode.BadRequest, "BUSSINESS"){}
    }
}