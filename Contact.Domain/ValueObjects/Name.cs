using Contact.Domain.Exceptions;
using DomainBase;
using System.Collections.Generic;

namespace Contact.Domain.ValueObjects
{
    public class Name: ValueObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}
