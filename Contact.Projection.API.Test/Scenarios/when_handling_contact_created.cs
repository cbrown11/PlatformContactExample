using System;
using Contact.Domain.Events;
using Contact.Domain.Exceptions;
using Contact.Domain.ValueObjects;
using Contact.Projection.API.Handlers;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

using AutoMapper;
using Contact.Projection.API.Mapping;
using Contact.Projection.Services;

namespace Contact.Service.Tests.Scenarios
{
    public abstract class ContactCreatedEventSpec: HandlerSpec
    {
        protected static ContactCreated ContactCreatedEvent;
        protected static ContactCreatedHandler ContactCreatedHandler;
        protected static Exception CaughtException;

        Establish context = () =>
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapping>());
            var mapper = config.CreateMapper();
            ContactCreatedHandler = new ContactCreatedHandler(new ContactService(ContactRepository.Object), mapper);
        };
    }

    [Subject(typeof(ContactCreated), "Command Handler")]
    public class when_handling_contact_created : ContactCreatedEventSpec
    {
        Establish context = () =>
        {
            ContactCreatedEvent = new ContactCreated(AuditInfo,"1", "fooId", new Name("firstName", "lastName"), new DateTime(2001, 1, 1)
                , "foo@foo.com", new Address("2", "street", "city", "NN3 5YL", "GBR"));

        };
        Because of = () => ContactCreatedHandler.Handle(ContactCreatedEvent, Context);
        It should_have_saved_contact = () => ContactRepository.Verify(foo => foo.AddAsync(Moq.It.IsAny<Projection.Models.Contact>()), Times.Exactly(1));
        It should_have_saved_contact_with_user_id = () => SavedContact.UserId.ShouldEqual("fooId");
    }



}