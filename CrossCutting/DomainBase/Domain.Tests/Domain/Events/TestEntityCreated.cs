
using DomainBase;
using DomainBase.Interfaces;

namespace Domain.Tests.Domain.Events
{

    public class TestEntityCreated : DomainEvent, IDomainEvent
    {
        public string TestProperty { get; set; }

        // Need for deserialize 
        public TestEntityCreated() { }

        public TestEntityCreated(string id, AuditInfo auditInfo) : base(auditInfo, id)
        {
        }
    }
}
