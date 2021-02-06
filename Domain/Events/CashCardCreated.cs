using System;
using DomainBase;
using DomainBase.Interfaces;

namespace Domain.Events
{
    public class CashCardCreated : DomainEvent, IDomainEvent
    {
        public string ClientId { get; set; }
        // Need for deserialize 
        public CashCardCreated() { }

        public CashCardCreated(AuditInfo auditInfo, string id, string clientId, DateTime? timeStamp = null)
            : base(auditInfo, id, timeStamp)
        {
            ClientId = clientId;
        }
    }
}
