using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contact.Projection.Repositories
{
    public interface IContactRepository
    {
        Task<IEnumerable<Models.Contact>> ListAsync();
        Task AddAsync(Models.Contact contact);
        Task<Models.Contact> FindByIdAsync(string id);
        void Update(Models.Contact contact);
        void Remove(Models.Contact contact);
    }
}
