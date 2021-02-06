using Contact.Domain.ValueObjects;
using DomainBase;
using MessageBase;
using System;
using System.ComponentModel.DataAnnotations;

namespace Contact.Service.Command
{
    public class CreateContact :  ICommand
    {
        [Required]
        public string UserId { get; set; }
        public Name Name { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string EmailAddress { get; set; }
        public Address Address { get; set; }

        public AuditInfo AuditInfo { get; set; }

    }
}
