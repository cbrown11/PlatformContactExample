using Domain.Tests.Domain.Events;
using DomainBase;
using DomainBase.Interfaces;

namespace Domain.Tests.Domain.TestEntityAggregate
{
    public class TestEntityAggregate : AggregateRoot, IAggregate
    {
        public  string Id { get; set; }
        public override string AggregateId
        {
            get { return Id; }
        }

        public  string TestProperty { get; set; }

        public TestEntityAggregate()
        {
            RegisterTransition<TestEntityCreated>(Apply);
            RegisterTransition<TestEntityModified>(Apply);
        }

        private void Apply(TestEntityCreated obj)
        {
            this.Id = obj.Id;
        }

        private void Apply(TestEntityModified obj)
        {
            this.TestProperty = obj.TestProperty;
        }

        public void Create(AuditInfo auditInfo, string id)
        {
            RaiseEvent(new TestEntityCreated(id,auditInfo));
        }

        public void Update(AuditInfo auditInfo, string id, string testProperty)
        {
            RaiseEvent(new TestEntityModified(id, auditInfo, testProperty));
        }

    }
}