
using GraphQL.Types;
using Platform.GraphQL.Helpers;

namespace Platform.GraphQL.Types.ObjectTypes
{
    public class CountryType : ObjectGraphType<ISO3166Country>
    {
        public CountryType()
        {
            Name = "Country";
            Field(x => x.Alpha2, type: typeof(StringGraphType));
            Field(x => x.Alpha3, type: typeof(StringGraphType));
            Field(x => x.Name, type: typeof(StringGraphType));
        }
    }
}
