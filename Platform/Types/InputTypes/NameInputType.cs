

using Contact.Domain.ValueObjects;
using GraphQL.Types;
using Platform.GraphQL.ValidationRules;

namespace Platform.GraphQL.InputTypes
{
    public class NameInputType : InputObjectGraphType<Name>
    {
        public NameInputType()
        {
            Name = "NameInput";
            Field(x => x.FirstName).Description("First Name")
                .Configure(type => type.Metadata.Add(nameof(NonEmptyStringValidationRule), null));
            Field(x => x.LastName).Description("Last Name")
                 .Configure(type => type.Metadata.Add(nameof(NonEmptyStringValidationRule), null));
        }
    }
}
