using GraphQL.Types;
using System.Globalization;
using Platform.GraphQL.Helpers;
namespace Platform.GraphQL.EnumTypes
{
    public class CountryEnum: EnumerationGraphType
    {
        public CountryEnum()
        {
 
            foreach (var country in  ISO3166.BuildCollection())
            {
   
                AddValue(country.Alpha3, country.Name, country.Alpha3);
            }
        }
    }
}
