using Contact.Domain.ValueObjects;
using DomainBase;
using MessageBase;
using System;

namespace Contact.Messages.Commands
{
    public class CreateContact : BusMessage, ICommand
    {
        public string UserId { get; set; }
        public Name Name { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string EmailAddress { get; set; }
        public Address Address { get; set; }

        public AuditInfo AuditInfo { get; set; }


        public CreateContact() { }

        public CreateContact(AuditInfo auditInfo, string userId,
            Name name, DateTimeOffset dateOfBirth, string emailAddress, Address address)
        {
            AuditInfo = auditInfo;
            UserId = userId;
            Name = name;
            DateOfBirth = dateOfBirth;
            EmailAddress = emailAddress;
            Address = address;
        }
    }
}
