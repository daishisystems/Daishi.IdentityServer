#region Includes

using Owin;
using Thinktecture.IdentityServer.Core.Configuration;
using Thinktecture.IdentityServer.Core.Logging;

#endregion

namespace Daishi.IdentityServer {
    public sealed class Startup {
        public void Configuration(IAppBuilder app) {
            LogProvider.SetCurrentLogProvider(new DiagnosticsTraceLogProvider());

            app.Map(
                "/core",
                coreApp => {
                    coreApp.UseIdentityServer(new IdentityServerOptions {
                        SiteName = "Standalone Identity Server",
                        SigningCertificate = Cert.Load(),
                        Factory = InMemoryFactory.Create(
                            Users.Get(),
                            Clients.Get(),
                            Scopes.Get()),
                        RequireSsl = true
                    });
                });
        }
    }
}