
using Platform.GraphQL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Platform.GraphQL.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            this._contactRepository = contactRepository;
        }

        Task<Contact.Projection.Models.Contact> IContactService.GetByIdAsync(string contactId)
        {
            return _contactRepository.FindByIdAsync(contactId);
        }

        Task<IEnumerable<Contact.Projection.Models.Contact>> IContactService.ListAsync()
        {
            return _contactRepository.ListAsync();
        }
    }
}
