#region Includes

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Thinktecture.IdentityServer.Core;
using Thinktecture.IdentityServer.Core.Extensions;
using Thinktecture.IdentityServer.Core.Models;
using Thinktecture.IdentityServer.Core.Services;

#endregion

namespace Daishi.IdentityServer {
    public class User {
        public string Subject { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Claim> Claims { get; set; }
    }

    public class RyanairUserService : IUserService {
        private readonly List<User> _users;

        public RyanairUserService() {
            _users = new List<User> {
                new User {
                    Subject = "1",
                    Username = "mooneyp",
                    Password = "password",
                    Claims = new List<Claim> {
                        new Claim(Constants.ClaimTypes.GivenName, "Paul"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Mooney"),
                        new Claim(Constants.ClaimTypes.Email, "mooneyp@ryanair.com"),
                    }
                }
            };
        }

        public Task<AuthenticateResult> AuthenticateExternalAsync(ExternalIdentity externalUser, SignInMessage message) {
            return Task.FromResult<AuthenticateResult>(null);
        }

        public Task<AuthenticateResult> AuthenticateLocalAsync(string username, string password, SignInMessage message) {
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            if (user == null) {
                return Task.FromResult<AuthenticateResult>(null);
            }
            return Task.FromResult<AuthenticateResult>(new AuthenticateResult(user.Subject, user.Username));
        }

        public Task<IEnumerable<Claim>> GetProfileDataAsync(ClaimsPrincipal subject, IEnumerable<string> requestedClaimTypes = null) {
            var user = _users.SingleOrDefault(x => x.Subject == subject.GetSubjectId());

            if (user == null) {
                return Task.FromResult<IEnumerable<Claim>>(null);
            }
            return Task.FromResult(user.Claims.Where(x => requestedClaimTypes.Contains(x.Type)));
        }

        public Task<bool> IsActiveAsync(ClaimsPrincipal subject) {
            var user = _users.SingleOrDefault(x => x.Subject == subject.GetSubjectId());
            return Task.FromResult(user != null);
        }

        public Task<AuthenticateResult> PreAuthenticateAsync(SignInMessage message) {
            return Task.FromResult<AuthenticateResult>(null);
        }

        public Task SignOutAsync(ClaimsPrincipal subject) {
            return Task.FromResult(0);
        }
    }
}