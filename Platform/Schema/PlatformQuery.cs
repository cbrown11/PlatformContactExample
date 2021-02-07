using System;
using Contact.Projection.Services;
using GraphQL;
using GraphQL.Types;
using Platform.GraphQL.Helpers;
using Platform.GraphQL.Services;
using Platform.GraphQL.Types.ObjectTypes;

namespace Platform.GraphQL
{
    public class PlatformQuery : ObjectGraphType<object>
    {
        public PlatformQuery(Services.IContactService contact)
        {
            Name = "Query";


            Field<ContactType>(
                    "contact",
                    arguments: new QueryArguments(
                        new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "id of the contact" }
                    ),
                    resolve: context => contact.GetByIdAsync(context.GetArgument<string>("id"))
                );

            Field<ListGraphType<ContactType>>(
                    "contacts",
                    resolve: context => contact.ListAsync()
                );

            Field<ListGraphType<CountryType>>(
                    "countries",
                    resolve: context => ISO3166.BuildCollection()
                );

        }
    }
}