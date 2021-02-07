
using GraphQL.Types;

namespace Platform.GraphQL.Types.ObjectTypes
{

    // Doesnt have to explicit reference the Contact.Projection.Models.Contact
    public class ContactType : ObjectGraphType<Contact.Projection.Models.Contact>
    {
        public ContactType()
        {
            Name = "Contact";
            Field(x => x.ContactId);
            Field(x => x.UserId);
            Field(x => x.FirstName);
            Field(x => x.LastName);
            Field(x => x.EmailAddress);
            Field(x => x.DateOfBirth, type: typeof(DateGraphType));
            Field(x => x.Address, type: typeof(AddressType));

        }
    }
}
