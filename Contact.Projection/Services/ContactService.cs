using Contact.Projection.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Projection.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            this._contactRepository = contactRepository;
        }


        public async Task<Models.Contact> GetByIdAsync(string id)
        {
            return await _contactRepository.FindByIdAsync(id);
        }

        public async Task<IEnumerable<Models.Contact>> ListAsync()
        {
            return await _contactRepository.ListAsync();
        }

        public async Task SaveAsync(Models.Contact contact)
        {
            if(_contactRepository.FindByIdAsync(contact.ContactId).Result == null)
                    _contactRepository.AddAsync(contact);
            else
                _contactRepository.Update(contact);
        }
    }
}
