using Contact.Domain.Events;
using Contact.Domain.Exceptions;
using Contact.Domain.ValueObjects;
using DomainBase;
using DomainBase.Interfaces;
using System;

namespace Contact.Domain
{
    public class Contact : AggregateRoot, IAggregate
    {
        public override string AggregateId => ContactId;
        public string ContactId { get; set; }
        public string UserId { get; set; }
        public Name Name { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string EmailAddress { get; set; }
        public Address Address { get; set; }

        public DateTime Created { get; private set; }

        public Contact()
        {
            RegisterTransition<ContactCreated>(Apply);
        }

        private void Apply(ContactCreated obj)
        {
            ContactId = obj.ContactId;
            UserId = obj.UserId;
            Name = obj.Name;
            DateOfBirth = obj.DateOfBirth;
            EmailAddress = obj.EmailAddress;
            Address = obj.Address;
            Created = DateTime.UtcNow;
        }

        public Contact(AuditInfo auditInfo, string userId, Name name, DateTimeOffset dateOfBirth, string emailAddress,
            Address address, DateTime timeStamp) : this()
        {
            var contactId = Guid.NewGuid().ToString();
            // This is domain logic but an example. data validation should be in the outer la and not the responsible of the domain. 
            if (string.IsNullOrEmpty(userId)) throw new ContactDomainException($"It not possible to create a contact without a userId");
            RaiseEvent(new ContactCreated(auditInfo, contactId, userId, name, dateOfBirth, emailAddress, address, timeStamp));
        }

        public static Contact Create(AuditInfo auditInfo, string userId, Name name, DateTimeOffset dateOfBirth, string emailAddress,
            Address address, DateTime timeStamp)
        {
            return new Contact(auditInfo, userId, name, dateOfBirth, emailAddress, address, timeStamp);
        }

    }
}
