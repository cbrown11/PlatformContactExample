using Contact.Projection.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Projection.Persistence.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly Dictionary<string, Models.Contact> _contacts = new Dictionary<string,Models.Contact>();

        public async Task AddAsync(Models.Contact contact)
        {
           _contacts.Add(contact.ContactId, contact);
        }

        public async  Task<Models.Contact> FindByIdAsync(string id)
        {
            if (!_contacts.ContainsKey(id)) return null;
            return _contacts[id];
        }

        public async Task<IEnumerable<Models.Contact>> ListAsync()
        {
            return _contacts.Values;
        }

        public void Remove(Models.Contact contact)
        {
            if(_contacts.ContainsKey(contact.ContactId))
                _contacts.Remove(contact.ContactId);
        }

        public void Update(Models.Contact contact)
        {
            if (_contacts.ContainsKey(contact.ContactId))
                _contacts[contact.ContactId] = contact;
        }
    }
}
