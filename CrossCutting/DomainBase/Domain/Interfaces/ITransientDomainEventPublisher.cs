
using System.Threading.Tasks;

namespace DomainBase.Interfaces
{
    public interface ITransientDomainEventPublisher
    {
        Task PublishAsync<T>(T publishedEvent);
    }

}
