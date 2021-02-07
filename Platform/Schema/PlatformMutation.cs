
using Contact.Messages.Commands;
using GraphQL;
using GraphQL.Types;
using NServiceBus;
using Platform.GraphQL.InputTypes;

namespace Platform.GraphQL
{ 

    public class PlatformMutation : ObjectGraphType
    {
        public PlatformMutation(IMessageSession messageSession)
        {
            Name = "Mutation";

            Field<StringGraphType>(
                "createContact",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<CreateContactInputType>> { Name = "createContact" }
                ),
                resolve: context =>
                {
                    var createContact = context.GetArgument<CreateContact>("createContact");
                    createContact.AuditInfo = new DomainBase.AuditInfo
                    {
                        By = "anonymous"
                    };
                    messageSession.Send(createContact).Wait();
                    return createContact.UserId;

                });
        }
    }
}