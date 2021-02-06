


using System;
using DomainBase;
using DomainBase.Interfaces;
using Machine.Specifications;
using Moq;
using NServiceBus.Testing;

namespace Contact.Service.Tests.Scenarios
{
    public abstract class HandlerSpec
    {
        protected static Mock<IDomainRepository> Repository;
        protected static DateTime DefaultDate = DateTime.UtcNow;
        protected static TestableMessageHandlerContext Context;
        protected static Domain.Contact SavedContact;

        protected static AuditInfo AuditInfo = new AuditInfo
        {
            Created = DefaultDate,
            By = "userName"
        };

        protected Establish context = () =>
        {
            Context = new TestableMessageHandlerContext();
            Repository = new Mock<IDomainRepository>();
            Repository.Setup(c => c.Save(Moq.It.IsAny<Domain.Contact>(), Moq.It.IsAny<bool>())).Callback<Domain.Contact, bool>((obj, isInitial)
                => SavedContact = obj
            );

        };
        private Cleanup after = () => { };
    }

}