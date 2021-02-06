

using System;

namespace Contact.Domain.Exceptions
{
    public class ContactDomainException : Exception
    {
        public ContactDomainException(string message) : base(message)
        {
        }

        public ContactDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
