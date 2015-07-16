#region Includes

using System;
using System.Security.Cryptography.X509Certificates;

#endregion

namespace Daishi.IdentityServer {
    internal static class Cert {
        public static X509Certificate2 Load() {
            return new X509Certificate2(
                string.Format(@"{0}\idsrv3test.pfx", AppDomain.CurrentDomain.BaseDirectory), "idsrv3test");
        }
    }
}