using System;
using System.Threading.Tasks;
using Contact.Messages.Commands;
using DomainBase.Exception;
using DomainBase.Interfaces;

using NServiceBus;
using NServiceBus.Logging;

namespace Contact.Service.Handlers
{
    public class CreateContactHandler : IHandleMessages<CreateContact>
    {
        protected IDomainRepository _domainRepository;

        static ILog log = LogManager.GetLogger<CreateContactHandler>();

        public CreateContactHandler(IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository;
        }

        public Task Handle(CreateContact message, IMessageHandlerContext context)
        {
            var contact = Domain.Contact.Create(message.AuditInfo, message.UserId,
                               message.Name, message.DateOfBirth, message.EmailAddress, message.Address,
                               DateTime.UtcNow);
            _domainRepository.Save(contact);
            log.Info($"contact {contact.ContactId} has now been created with {contact.UserId}");
            return Task.CompletedTask;
        }
    }
}
