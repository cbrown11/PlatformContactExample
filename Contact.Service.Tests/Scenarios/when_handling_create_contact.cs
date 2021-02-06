using System;
using Contact.Domain.Exceptions;
using Contact.Domain.ValueObjects;
using Contact.Messages.Commands;
using Contact.Service.Handlers;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Contact.Service.Tests.Scenarios
{
    public abstract class CreateContactCommandSpec: HandlerSpec
    {
        protected static CreateContact CreateContactCommand;
        protected static CreateContactHandler CreateContactHandler;
        protected static Exception CaughtException;

        Establish context = () =>
        {
            CreateContactHandler = new CreateContactHandler(Repository.Object);

        };
    }

    [Subject(typeof(CreateContact), "Command Handler")]
    public class when_handling_create_contact_command : CreateContactCommandSpec
    {
        Establish context = () =>
        {
            CreateContactCommand = new CreateContact(AuditInfo, "fooId", new Name("firstName", "lastName"), new DateTime(2001, 1, 1)
                , "foo@foo.com", new Address("2", "street", "city", "NN3 5YL", "GBR"));

        };
        Because of = () => CreateContactHandler.Handle(CreateContactCommand, Context);
        It should_have_saved_contact = () => Repository.Verify(foo => foo.Save(Moq.It.IsAny<Domain.Contact>(), false), Times.Exactly(1));
        It should_have_saved_contact_with_user_id = () => SavedContact.UserId.ShouldEqual("fooId");
    }

    [Subject(typeof(CreateContact), "Command Handler")]
    public class when_handling_create_contact_command_with_empty_userId : CreateContactCommandSpec
    {
        Establish context = () =>
        {
            CreateContactCommand = new CreateContact(AuditInfo, "", new Name("firstName", "lastName"), new DateTime(2001, 1, 1)
                , "foo@foo.com", new Address("2", "street", "city", "NN3 5YL", "GBR"));
        };
        Because of = () => CaughtException = Catch.Exception(() => CreateContactHandler.Handle(CreateContactCommand, Context));
        It should_have_raised_argument_exception = () => CaughtException.ShouldBeOfExactType<ContactDomainException>();
        It should_have_not_saved_contact = () => Repository.Verify(foo => foo.Save(Moq.It.IsAny<Domain.Contact>(), false), Times.Exactly(0));
    }

}