using System;

namespace DomainBase.Interfaces
{
    public interface IDomainEvent
    {
        string Id { get; }

        DateTime TimeStamp { get; set; }

        AuditInfo AuditInfo { get; set; }

    }
}
