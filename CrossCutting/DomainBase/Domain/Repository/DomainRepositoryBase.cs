using System;
using System.Collections.Generic;
using DomainBase.Interfaces;

namespace DomainBase.Repository
{
    public abstract class DomainRepositoryBase : IDomainRepository
    {
        protected  readonly string Category;
        public abstract IEnumerable<IDomainEvent> Save<TAggregate>(TAggregate aggregate, bool isInitial = false) where TAggregate : IAggregate;
        public abstract bool Exists<TResult>(string id) where TResult : IAggregate, new();
   
        public abstract TResult GetById<TResult>(string id) where TResult : IAggregate, new();
        public abstract string GetLast<TResult>() where TResult : IAggregate, new();

        protected   DomainRepositoryBase(string category)
        {
            Category = category;
        }


        protected virtual string AggregateToStreamName(Type type, string id)
        {
            return string.Format("{0}-{1}-{2}", Category, type.Name, id);
        }

        protected virtual long CalculateExpectedVersion<T>(IAggregate aggregate, List<T> events)
        {
            var originalVersion = aggregate.Version - events.Count;
            long expectedVersion = originalVersion == -1 ? -1 : originalVersion;
            return expectedVersion;
        }

        protected TResult BuildAggregate<TResult>(IEnumerable<IDomainEvent> events) where TResult : IAggregate, new()
        {
            var result = new TResult();
            foreach (var @event in events)
            {
                result.ApplyEvent(@event);
            }
            return result;
        }
    }
}