using System;
using System.Threading.Tasks;
using Contact.Projection.Repositories;
using DomainBase;
using Machine.Specifications;
using Moq;
using NServiceBus.Testing;

namespace Contact.Service.Tests.Scenarios
{
    public abstract class HandlerSpec
    {
        protected static Mock<IContactRepository> ContactRepository;
        protected static DateTime DefaultDate = DateTime.UtcNow;
        protected static TestableMessageHandlerContext Context;
        protected static Projection.Models.Contact SavedContact;

        protected static AuditInfo AuditInfo = new AuditInfo
        {
            Created = DefaultDate,
            By = "userName"
        };

        protected Establish context = () =>
        {
            Context = new TestableMessageHandlerContext();
            ContactRepository = new Mock<IContactRepository>();
            ContactRepository.Setup(c =>  c.AddAsync(Moq.It.IsAny<Projection.Models.Contact>()))
            .Callback<Projection.Models.Contact>((obj)
                => SavedContact = obj
            )
           .Returns<Projection.Models.Contact>((obj)=>Task.FromResult(obj));
            ;

          };
        private Cleanup after = () => { };
    }

}