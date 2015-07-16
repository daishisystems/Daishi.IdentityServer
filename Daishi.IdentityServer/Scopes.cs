#region Includes

using System.Collections.Generic;
using Thinktecture.IdentityServer.Core.Models;

#endregion

namespace Daishi.IdentityServer {
    public static class Scopes {
        public static IEnumerable<Scope> Get() {
            return new List<Scope> {
                StandardScopes.OpenId,
                StandardScopes.Profile,
                StandardScopes.Email
            };
        }
    }
}