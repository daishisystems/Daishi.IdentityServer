#region Includes

using System.Web;
using System.Web.Mvc;

#endregion

namespace Daishi.FormPostClient.Controllers {
    public class AccountController : Controller {
        [Authorize]
        public ActionResult SignIn() {
            return this.Redirect("/");
        }

        public ActionResult Callback(string code, string session_state) {
            return this.Redirect("/");
        }

        public ActionResult SignOut() {
            this.Request.GetOwinContext().Authentication.SignOut();
            return this.Redirect("/");
        }
    }
}