using System;

namespace Contact.Projection.Models
{
    public class Contact
    {
        public string ContactId { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string EmailAddress { get; set; }
        public Address Address { get; set; }
    }
}
