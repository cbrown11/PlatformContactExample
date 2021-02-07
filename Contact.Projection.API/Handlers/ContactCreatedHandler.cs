using AutoMapper;
using Contact.Domain.Events;
using Contact.Projection.Services;
using NServiceBus;
using System;
using System.Threading.Tasks;

namespace Contact.Projection.API.Handlers
{
    public class ContactCreatedHandler : IHandleMessages<ContactCreated>
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        public ContactCreatedHandler(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;

        }
        public async Task Handle(ContactCreated message, IMessageHandlerContext context)
        {
            var contact = _mapper.Map<Models.Contact>(message);
            await _contactService.SaveAsync(contact);
        }
    }
}
