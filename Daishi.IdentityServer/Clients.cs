﻿#region Includes

using System.Collections.Generic;
using Thinktecture.IdentityServer.Core;
using Thinktecture.IdentityServer.Core.Models;

#endregion

namespace Daishi.IdentityServer {
    public static class Clients {
        public static IEnumerable<Client> Get() {
            return new List<Client> {
                new Client {
                    ClientId = @"hybridclient",
                    ClientName = @"Ryanair",
                    ClientSecrets = new List<ClientSecret> {
                        new ClientSecret("idsrv3test".Sha256())
                    },
                    Enabled = true,
                    Flow = Flows.Hybrid,
                    RequireConsent = false,
                    AllowRememberConsent = true,
                    RedirectUris = new List<string> {
                        "https://localhost:44304/"
                    },
                    PostLogoutRedirectUris = new List<string> {
                        "https://localhost:44304/"
                    },
                    ScopeRestrictions = new List<string> {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        Constants.StandardScopes.Email,
                        Constants.StandardScopes.Roles,
                        Constants.StandardScopes.OfflineAccess
                    },
                    AccessTokenType = AccessTokenType.Jwt
                },
                new Client {
                    ClientId = "ROCGClient",
                    ClientName = "Ryanair",
                    ClientSecrets = new List<ClientSecret> {
                        new ClientSecret("password".Sha256())
                    },
                    Flow = Flows.ResourceOwner,
                    RequireConsent = false,
                    RedirectUris = new List<string> {
                        "https://localhost:44304/account/callback"
                    }
                },
                new Client {
                    ClientId = "codeClient",
                    ClientName = "Ryanair",
                    ClientSecrets = new List<ClientSecret> {
                        new ClientSecret("password".Sha256())
                    },
                    Flow = Flows.AuthorizationCode,
                    RequireConsent = false,
                    RedirectUris = new List<string> {
                        "https://localhost:44304/account/callback",
                        "https://localhost:9443/examples/callback"
                    }
                }
            };
        }
    }
}