using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(CareerProject.Areas.Admin.Models.Startup))]


namespace CareerProject.Areas.Admin.Models
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Account/Login")
            });

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "871999803724-j345ee19vot360c8h6kf5orajc1umfep.apps.googleusercontent.com",
                ClientSecret = "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",
                CallbackPath = new PathString("/signin-google")
            });
        }
    }
}