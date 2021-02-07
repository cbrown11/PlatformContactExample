using System.Collections.Generic;
using System.Threading.Tasks;

namespace Platform.GraphQL.Repositories
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact.Projection.Models.Contact>> ListAsync();
        Task<Contact.Projection.Models.Contact> FindByIdAsync(string id);
    }
}
