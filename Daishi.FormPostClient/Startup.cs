﻿#region Includes

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Web.Helpers;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using Thinktecture.IdentityModel.Client;

#endregion

namespace Daishi.FormPostClient {
    public class Startup {
        private const string ClientUri = @"https://localhost:44304/";
        private const string CallbackEndpoint = @"/account/signInCallback";
        private const string IdServBaseUri = @"https://identitydev01.azurewebsites.net/core";
        private const string AuthorizeUri = IdServBaseUri + @"/connect/authorize";
        private const string LogoutUri = IdServBaseUri + @"/connect/endsession";
        private const string tokenEndpoint = IdServBaseUri + @"/connect/token";
        private const string userInfoEndpoint = IdServBaseUri + @"/connect/userinfo";

        public void Configuration(IAppBuilder app) {
            AntiForgeryConfig.UniqueClaimTypeIdentifier = "sub";
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.UseCookieAuthentication(new CookieAuthenticationOptions {
                AuthenticationType = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions {
                    ClientId = "hybridclient",
                    Authority = IdServBaseUri,
                    RedirectUri = ClientUri,
                    PostLogoutRedirectUri = ClientUri,
                    ResponseType = "code id_token token",
                    Scope = "openid profile email roles offline_access",
                    SignInAsAuthenticationType = "Cookies",
                    Notifications =
                        new OpenIdConnectAuthenticationNotifications {
                            AuthorizationCodeReceived = async n => {
                                var identity = n.AuthenticationTicket.Identity;
                                var nIdentity = new ClaimsIdentity(identity.AuthenticationType, "email", "role");

                                var userInfoClient = new UserInfoClient(
                                    new Uri(userInfoEndpoint),
                                    n.ProtocolMessage.AccessToken);

                                var userInfo = await userInfoClient.GetAsync();
                                userInfo.Claims.ToList().ForEach(x => nIdentity.AddClaim(new Claim(x.Item1, x.Item2)));

                                var tokenClient = new OAuth2Client(new Uri(tokenEndpoint), "hybridclient", "idsrv3test");
                                var response = await tokenClient.RequestAuthorizationCodeAsync(n.Code, n.RedirectUri);

                                nIdentity.AddClaim(new Claim("access_token", response.AccessToken));
                                nIdentity.AddClaim(
                                    new Claim("expires_at", DateTime.UtcNow.AddSeconds(response.ExpiresIn).ToLocalTime().ToString(CultureInfo.InvariantCulture)));
                                nIdentity.AddClaim(new Claim("refresh_token", response.RefreshToken));
                                nIdentity.AddClaim(new Claim("id_token", n.ProtocolMessage.IdToken));

                                n.AuthenticationTicket = new AuthenticationTicket(
                                    nIdentity,
                                    n.AuthenticationTicket.Properties);
                            },
                            RedirectToIdentityProvider = async n => {
                                if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest) {
                                    var idTokenHint = n.OwinContext.Authentication.User.FindFirst("id_token").Value;
                                    n.ProtocolMessage.IdTokenHint = idTokenHint;
                                }
                            }
                        }
                });
        }
    }
}