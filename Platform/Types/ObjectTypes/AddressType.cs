using CrossCutting.Country;
using GraphQL.Types;

namespace Platform.GraphQL.Types.ObjectTypes
{
    public class AddressType : ObjectGraphType<Contact.Projection.Models.Address>
    {
        public AddressType()
        {
            Name = "Address";
            Description = "Address details";
            Field(x => x.HouseNameNumber);
            Field(x => x.Street);
            Field(x => x.Postcode);
            Field(x => x.City);
            Field(typeof(CountryType), "Country", resolve: context => ISO3166.FromAlpha3(context.Source.CountryIsoAlpha3));
        }
    }
}
