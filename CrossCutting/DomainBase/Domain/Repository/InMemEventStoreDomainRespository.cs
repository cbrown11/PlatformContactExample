using System;
using System.Collections.Generic;
using System.Linq;
using DomainBase.Exception;
using DomainBase.Interfaces;
using Newtonsoft.Json;

namespace DomainBase.Repository
{
    public class InMemEventStoreDomainRespository : DomainRepositoryBase
    {
        public Dictionary<string, List<string>> _eventStore = new Dictionary<string, List<string>>();
        private List<IDomainEvent> _latestEvents = new List<IDomainEvent>();
        private JsonSerializerSettings _serializationSettings;
        private readonly ITransientDomainEventPublisher _publisher;

        public InMemEventStoreDomainRespository(string category, ITransientDomainEventPublisher publisher = null) 
            : base(category)
        {
            this._publisher = publisher;
            _serializationSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
        }

        public override IEnumerable<IDomainEvent> Save<TAggregate>(TAggregate aggregate, bool isInitial = false)
        {
            var uncommitedEvents = aggregate.UncommitedEvents().ToList();
            var serializedEvents = uncommitedEvents.Select(Serialize).ToList();
            var expectedVersion = CalculateExpectedVersion(aggregate, uncommitedEvents);
            if (expectedVersion < 0)
            {
                _eventStore.Add(aggregate.AggregateId, serializedEvents);
            }
            else
            {
                var existingEvents = _eventStore[aggregate.AggregateId];
                var currentversion = existingEvents.Count - 1;
                if (currentversion != expectedVersion)
                {
                    throw new System.Exception("Expected version " + expectedVersion + " but the version is " + currentversion);
                }
                existingEvents.AddRange(serializedEvents);
            }
            _latestEvents.AddRange(uncommitedEvents);
            aggregate.ClearUncommitedEvents();
            foreach (var _event in uncommitedEvents)
                PublishEvent(_event);
            return uncommitedEvents;
        }

        public override bool Exists<TResult>(string id)
        {
            return _eventStore.ContainsKey(id);
        }

        private string Serialize(IDomainEvent arg)
        {
            return JsonConvert.SerializeObject(arg, _serializationSettings);
        }

        public IEnumerable<IDomainEvent> GetLatestEvents()
        {
            return _latestEvents;
        }

        protected void PublishEvent(object @event)
        {
            if (_publisher != null)
                _publisher.PublishAsync((dynamic)@event).Wait();
        }


        public override TResult GetById<TResult>(string id)
        {
            if (_eventStore.ContainsKey(id))
            {
                var events = _eventStore[id];
                var deserializedEvents = events.Select(e => JsonConvert.DeserializeObject(e, _serializationSettings) as IDomainEvent);
                return BuildAggregate<TResult>(deserializedEvents);
            }
            throw new AggregateNotFoundException("Could not found aggregate of type " + typeof(TResult) + " and id " + id);
        }

        public override string GetLast<TResult>()
        {
            return _eventStore.Last().Key;
        }
    }
}
