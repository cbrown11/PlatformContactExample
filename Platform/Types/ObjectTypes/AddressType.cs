using GraphQL.Types;

namespace Platform.GraphQL.Types.ObjectTypes
{
    public class AddressType : ObjectGraphType<Contact.Projection.Models.Address>
    {
        public AddressType()
        {
            Name = "Address";
            Field(x => x.HouseNameNumber);
            Field(x => x.Street);
            Field(x => x.Postcode);
            Field(x => x.City);

        }
    }
}
