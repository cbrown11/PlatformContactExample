using System;
using DomainBase;
using Machine.Specifications;
using Contact.Domain.ValueObjects;
using Contact.Domain.Exceptions;

namespace CashCardExample.UnitTests.Scenarios.Domains
{
    public abstract class ContactSpec
    {
        protected static Contact.Domain.Contact SUT;
        protected static DateTime DefaultDate = DateTime.UtcNow;
        protected static AuditInfo AuditInfo = new AuditInfo
        {
            Created = DefaultDate,
            By = "userName"
        };

        Establish context = () =>
        {
            SUT = new Contact.Domain.Contact(AuditInfo,"fooId",new Name("firstName","lastName"),new DateTime(2001,1,1)
                ,"foo@foo.com", new Address("2", "street", "city", "NN3 5YL", "GBR"), DefaultDate);
        };
    }

    [Subject(typeof(Contact.Domain.Contact))]
    public class when_create_contact : ContactSpec
    {
        Because of = () => { };
        It should_set_clientId = () => SUT.ContactId.ShouldNotBeEmpty();
        It should_set_userId = () => SUT.UserId.ShouldEqual("fooId");
        It should_set_name_firstName = () => SUT.Name.FirstName.ShouldEqual("firstName");
        It should_set_name_lastName = () => SUT.Name.LastName.ShouldEqual("lastName");
        It should_set_dateOfBirth = () => SUT.DateOfBirth.ShouldEqual(new DateTime(2001, 1, 1));
        It should_set_emailAddress = () => SUT.EmailAddress.ShouldEqual("foo@foo.com");
        It should_set_Address_houseNameNumber = () => SUT.Address.HouseNameNumber.ShouldEqual("2");
        It should_set_Address_postcode = () => SUT.Address.Postcode.ShouldEqual("NN3 5YL");
        It should_set_Address_street = () => SUT.Address.Street.ShouldEqual("street");
        It should_set_Address_city = () => SUT.Address.City.ShouldEqual("city");
        It should_set_Address_countryIsoAlpha3 = () => SUT.Address.CountryIsoAlpha3.ShouldEqual("GBR");
    }

    [Subject(typeof(Contact.Domain.Contact))]
    public class when_create_contact_with_invalid_userId : ContactSpec
    {
        protected static Exception exception;

        Establish context = () =>
        {
            SUT = null;
        };

        Because of = () => exception = Catch.Exception(() => SUT = new Contact.Domain.Contact(AuditInfo, "", new Name("firstName", "lastName"), new DateTime(2001, 1, 1)
                , "foo@foo.com", new Address("2", "street", "city", "NN3 5YL", "GBR"), DefaultDate));
        It should_not_allow_withdrawal = () => exception.ShouldBeOfExactType<ContactDomainException>();
        It should_report_the_reason = () => exception.Message.ShouldEqual("It not possible to create a contact without a userId");
        It should_not_created = () => SUT.ShouldBeNull();
    }

    [Subject(typeof(Contact.Domain.Contact))]
    public class when_create_contact_with_invalid_countrycode : ContactSpec
    {
        protected static Exception exception;

        Establish context = () =>
        {
            SUT = null;
        };

        Because of = () => exception = Catch.Exception(() => SUT = new Contact.Domain.Contact(AuditInfo, "", new Name("firstName", "lastName"), new DateTime(2001, 1, 1)
                , "foo@foo.com", new Address("2", "street", "city", "NN3 5YL", ""), DefaultDate));
        It should_not_allow_withdrawal = () => exception.ShouldBeOfExactType<ArgumentException>();
        It should_report_the_reason = () => exception.Message.ShouldEqual("the country ISO Alpah must be a valid code");
        It should_not_created = () => SUT.ShouldBeNull();
    }

}
