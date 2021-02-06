using Contact.Projection.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Platform.GraphQL.Services
{
    public class ContactService : IContactService
    {
 
        Task<Contact.Projection.Models.Contact> IContactService.GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Contact.Projection.Models.Contact>> IContactService.ListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
