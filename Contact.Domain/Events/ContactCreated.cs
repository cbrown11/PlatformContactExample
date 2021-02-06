using Contact.Domain.ValueObjects;
using DomainBase;
using DomainBase.Interfaces;
using System;

namespace Contact.Domain.Events
{
    public class ContactCreated : DomainEvent, IDomainEvent
    {
        public string ContactId { get; set; }
        public string UserId { get; set; }
        public Name Name { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string EmailAddress { get; set; }
        public Address Address { get; set; }

        // Need for deserialize 
        public ContactCreated() { }

        public ContactCreated(AuditInfo auditInfo, string contactId, string userId,
            Name name, DateTimeOffset dateOfBirth, string emailAddress, Address address, DateTime? timeStamp = null)
            : base(auditInfo, contactId, timeStamp)
        {
            ContactId = contactId;
            UserId = userId;
            Name = name;
            DateOfBirth = dateOfBirth;
            EmailAddress = emailAddress;
            Address = address;
        }
    }
}
