using System;
using System.Collections.Generic;
using DomainBase;
using DomainBase.Interfaces;

namespace Domain.Events
{
    public class MoneyWithdrawn : DomainEvent, IDomainEvent
    {
        public string CardNumber => Id;

        public double Quantity { get; set; }

        // Need for deserialize 
        public MoneyWithdrawn() { }

        public MoneyWithdrawn(AuditInfo auditInfo, string id, double quantity, DateTime timeStamp)
            : base(auditInfo, id, timeStamp)
        {
            Quantity = quantity;
        }
    }
}
