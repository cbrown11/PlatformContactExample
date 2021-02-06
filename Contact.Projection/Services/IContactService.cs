using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contact.Projection.Services
{
    public interface IContactService
    {
        Task<IEnumerable<Models.Contact>> ListAsync();
        Task<Models.Contact> GetByIdAsync(string v);
    }
}