using System.Security.Cryptography;

namespace domain.exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) {}
    }
}