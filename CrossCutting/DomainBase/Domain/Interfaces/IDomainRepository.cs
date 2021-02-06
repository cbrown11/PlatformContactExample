using System.Collections.Generic;

namespace DomainBase.Interfaces
{
    public interface IDomainRepository
    {
        IEnumerable<IDomainEvent> Save<TAggregate>(TAggregate aggregate, bool isInitial = false) where TAggregate : IAggregate;
        bool Exists<TResult>(string id) where TResult : IAggregate, new();
        TResult GetById<TResult>(string id) where TResult : IAggregate, new();
        string GetLast<TResult>() where TResult : IAggregate, new();
    }
}