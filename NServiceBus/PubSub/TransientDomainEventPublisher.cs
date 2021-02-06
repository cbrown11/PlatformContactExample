using System.Threading.Tasks;
using DomainBase.Interfaces;
using NServiceBus;

namespace CrossCutting.NserviceBus.PubSub
{
    public class TransientDomainEventPublisher : ITransientDomainEventPublisher
    {
        private readonly IEndpointInstance _endpoint;

        public TransientDomainEventPublisher(IEndpointInstance endpoint)
        {
            _endpoint = endpoint;
        }

        public async Task PublishAsync<T>(T publishedEvent)
        {
            await _endpoint.Publish(publishedEvent);
        }

        public async Task PublishAsync(object publishedEvent)
        {
            await _endpoint.Publish(publishedEvent);
        }
    }
}
