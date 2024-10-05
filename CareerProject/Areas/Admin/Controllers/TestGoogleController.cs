using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace CareerProject.Areas.Admin.Controllers
{
    public class TestGoogleController : Controller
    {
        // GET: Admin/TestGoogle
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        public ActionResult GoogleLogin(string name, string email)
        {
            string Name = name;
            string Email = email;

            return View();
        }

        public ActionResult GoogleLoginCallback(string returnUrl)
        {
            var loginInfo = AuthenticationManager.AuthenticateAsync("ExternalCookie").Result;
            if (loginInfo != null)
            {
                var claimsIdentity = new ClaimsIdentity(loginInfo.Identity.Claims, "ApplicationCookie");
                AuthenticationManager.SignIn(claimsIdentity);

                return RedirectToLocal(returnUrl);
            }

            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut("ApplicationCookie");
            return RedirectToAction("Index", "Home");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }

}