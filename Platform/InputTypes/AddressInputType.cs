

using Contact.Domain.ValueObjects;
using GraphQL.Types;
using Platform.GraphQL.EnumTypes;

namespace Platform.GraphQL.InputTypes
{
    public class AddressInputType : InputObjectGraphType<Address>
    {
        public AddressInputType()
        {
            Name = "AddressInput";
            Field(x => x.HouseNameNumber);
            Field(x => x.Street);
            Field(x => x.Postcode);
            Field(x => x.City, nullable: true);
            //  Field(x => x.CountryIsoAlpha3);

            Field(x => x.CountryIsoAlpha3, type: typeof(NonNullGraphType<CountryEnum>), nullable:false)
                .Description("country alpha 3 code of the address");
        }
    }
}
