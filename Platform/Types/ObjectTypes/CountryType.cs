
using CrossCutting.Country;
using GraphQL.Types;

namespace Platform.GraphQL.Types.ObjectTypes
{
    public class CountryType : ObjectGraphType<ISO3166Country>
    {
        public CountryType()
        {
            Name = "Country";
            Description = "country name and ISO codes";
            Field(x => x.Alpha2, type: typeof(StringGraphType));
            Field(x => x.Alpha3, type: typeof(StringGraphType));
            Field(x => x.Name, type: typeof(StringGraphType));
        }
    }
}
