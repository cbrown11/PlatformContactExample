
using GraphQL.Types;

namespace Platform.GraphQL.Types
{

    // Doesnt have to explicit reference the Contact.Projection.Models.Contact
    public class ContactType : ObjectGraphType<Contact.Projection.Models.Contact>
    {
        public ContactType()
        {
            Name = "Contact";
            Field(x => x.UserId);
            Field(x => x.FirstName);
            Field(x => x.LastName);
        }
    }
}
