#region Includes

using System.Collections.Generic;
using Thinktecture.IdentityServer.Core;
using Thinktecture.IdentityServer.Core.Models;

#endregion

namespace Daishi.IdentityServer {
    public static class Scopes {
        public static IEnumerable<Scope> Get() {
            return new List<Scope> {
                StandardScopes.OpenId,
                StandardScopes.Profile,
                StandardScopes.Email,
                StandardScopes.RolesAlwaysInclude,
                StandardScopes.OfflineAccess,                
                new Scope {
                    Name = "custom",
                    DisplayName = "Custom",
                    Description = "Custom Scope",
                    Type = ScopeType.Identity,
                    Claims = new List<ScopeClaim> {
                        new ScopeClaim(Constants.ClaimTypes.GivenName, true),
                        new ScopeClaim(Constants.ClaimTypes.FamilyName, true),
                        new ScopeClaim(Constants.ClaimTypes.Email, true)
                    }
                }
            };
        }
    }
}