using Contact.Messages.Commands;
using GraphQL.Types;
using Platform.GraphQL.ValidationRules;

namespace Platform.GraphQL.InputTypes
{
    public class CreateContactInputType : InputObjectGraphType<CreateContact>
    {
        public CreateContactInputType()
        {
            Name = "CreateContactCommand";

            Field(x => x.UserId)
                .Description("Provide an userId for the contact with no white space")
                .Configure(type=> type.Metadata.Add(nameof(NonEmptyStringAndWhiteSpaceValidationRule),null));
            Field(x => x.Name, type: typeof(NameInputType))
                 .Description("Provide an name for the contact");
            Field(x => x.EmailAddress)
                .Description("Provide a valid email address for the contact")
                .Configure(type => type.Metadata.Add(nameof(EmailAddressValidationRule), null));
            Field(x => x.DateOfBirth, type: typeof(NonNullGraphType<DateGraphType>))
                 .Description("Provide an date of birth for the contact");
            Field(x => x.Address, type: typeof(AddressInputType))
             .Description("Provide an address for the contact");
        }
    }
}
