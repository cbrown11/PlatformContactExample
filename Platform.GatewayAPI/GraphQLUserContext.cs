
using System.Collections.Generic;
using System.Security.Claims;

namespace Platform.GatewayAPI
{
    public class GraphQLUserContext : Dictionary<string, object>
    {
        public ClaimsPrincipal User { get; set; }
    }
}
