using System;
using DomainBase.Interfaces;

namespace DomainBase
{
    public class DomainEvent: IDomainEvent
    {
        public string Id { get; set; }

         public AuditInfo AuditInfo { get; set; }

        public DateTime TimeStamp { get; set; }

        // Need for deserialize 
        public DomainEvent()
        {
            TimeStamp = DateTime.UtcNow;
        }
  
        public DomainEvent(AuditInfo auditInfo,  string id, DateTime? timeStamp = null)
        {
            AuditInfo = auditInfo;
            if (!timeStamp.HasValue)
                TimeStamp = DateTime.UtcNow;
            else
                TimeStamp = (DateTime)timeStamp;
            Id = id;
        }
    }
}
