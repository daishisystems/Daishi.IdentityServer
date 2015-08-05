#region Includes

using Owin;
using Thinktecture.IdentityServer.Core.Configuration;
using Thinktecture.IdentityServer.Core.Logging;
using Thinktecture.IdentityServer.Core.Services;

#endregion

namespace Daishi.IdentityServer {
    public sealed class Startup {
        public void Configuration(IAppBuilder app) {
            LogProvider.SetCurrentLogProvider(new DiagnosticsTraceLogProvider());

            app.Map(
                "/core",
                coreApp => {
                    var factory = InMemoryFactory.Create(
                        clients: Clients.Get(),
                        scopes: Scopes.Get()
                        );
                    factory.UserService = new Registration<IUserService>(resolver => new RyanairUserService());

                    coreApp.UseIdentityServer(new IdentityServerOptions {
                        SiteName = "Ryanair Identity Provider",
                        SigningCertificate = Cert.Load(),
                        RequireSsl = true,
                        Factory = factory
                    });
                });
        }
    }
}