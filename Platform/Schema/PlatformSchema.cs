
using GraphQL.Types;
using GraphQL.Utilities;
using Platform.GraphQL;
using System;

namespace Platform
{
    public class PlatformSchema : Schema
    {
        public PlatformSchema(IServiceProvider provider)
            : base(provider)
        {
            Query = provider.GetRequiredService<PlatformQuery>();
            Mutation = provider.GetRequiredService<PlatformMutation>();
        }
    }

}
