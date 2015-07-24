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
                },
                new InMemoryUser {
                    Username = "novickyp@ryanair.com",
                    Password = "password",
                    Subject = "173a3d80",
                    Claims = new List<Claim> {
                        new Claim(Constants.ClaimTypes.Email, "novickyp@ryanair.com"),
                        new Claim(Constants.ClaimTypes.GivenName, "Petr"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Novicky")
                    }
                },
                new InMemoryUser {
                    Username = "stors@ryanair.com",
                    Password = "password",
                    Subject = "173a3d81",
                    Claims = new List<Claim> {
                        new Claim(Constants.ClaimTypes.Email, "stors@ryanair.com"),
                        new Claim(Constants.ClaimTypes.GivenName, "Sašo"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Štor")
                    }
                },
                new InMemoryUser {
                    Username = "ivancicm@ryanair.com",
                    Password = "password",
                    Subject = "173a3d82",
                    Claims = new List<Claim> {
                        new Claim(Constants.ClaimTypes.Email, "ivancicm@ryanair.com"),
                        new Claim(Constants.ClaimTypes.GivenName, "Martin"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Ivancic")
                    }
                }
            };
        }
    }
}