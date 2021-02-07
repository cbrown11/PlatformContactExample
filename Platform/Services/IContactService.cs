using System.Collections.Generic;
using System.Threading.Tasks;

namespace Platform.GraphQL.Services
{
    public interface IContactService
    {
        Task<IEnumerable<Contact.Projection.Models.Contact>> ListAsync();
        Task<Contact.Projection.Models.Contact> GetByIdAsync(string id);

    }
}