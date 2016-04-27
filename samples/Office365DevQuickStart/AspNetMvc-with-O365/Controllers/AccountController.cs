using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System;
using System.Web;
using System.Web.Mvc;

namespace AspNetMvc_with_O365.Controllers
{
    /// <summary>
    /// Controls sign-in and sign-out.
    /// </summary>

    public class AccountController : Controller
    {
        public void SignIn()
        {
            if (!Request.IsAuthenticated)
            {
                HttpContext.GetOwinContext().Authentication.Challenge(new AuthenticationProperties { RedirectUri = "/" }, OpenIdConnectAuthenticationDefaults.AuthenticationType);
            }
        }
        public void SignOut()
        {
            string callbackUrl = Url.Action("SignOutCallback", "Account", routeValues: null, protocol: Request.Url.Scheme);
            
            HttpContext.GetOwinContext().Authentication.SignOut(
                new AuthenticationProperties { RedirectUri = callbackUrl },
                OpenIdConnectAuthenticationDefaults.AuthenticationType, CookieAuthenticationDefaults.AuthenticationType);
        }

        public ActionResult SignOutCallback()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public void ClearCacheDatabase()
        {
            var context = new TokenCacheDbContext();

            foreach(var cacheItem in context.UserTokenCaches)
            {
                context.UserTokenCaches.Remove(cacheItem);
            }

            context.SaveChanges();

            string strRedirectController = Request.QueryString["redirect"];

            HttpContext.GetOwinContext().Authentication.Challenge(new AuthenticationProperties { RedirectUri = String.Format("/{0}", strRedirectController) }, OpenIdConnectAuthenticationDefaults.AuthenticationType);
        }
    }
}