#region Includes

using System.Collections.Generic;
using System.Security.Claims;
using Thinktecture.IdentityServer.Core;
using Thinktecture.IdentityServer.Core.Services.InMemory;

#endregion

namespace Daishi.IdentityServer {
    public static class Users {
        public static List<InMemoryUser> Get() {
            return new List<InMemoryUser> {
                new InMemoryUser {
                    Username = "admin",
                    Password = "password",
                    Subject = "1",
                    Claims = new List<Claim> {
                        new Claim(Constants.ClaimTypes.GivenName, "Administrator"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Ryanair"),
                        new Claim(Constants.ClaimTypes.Email, "admin@ryanair.com")
                    }
                }
            };
        }
    }
}