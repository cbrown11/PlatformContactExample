using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Projection.Services
{
    public class ContactService: IContactService
    {
        public Task<Models.Contact> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Models.Contact>> ListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
