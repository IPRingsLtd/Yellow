using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(_360Appraisal.Startup))]

namespace _360Appraisal
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(_360Appraisal.Models.AppContext.Create);
            app.CreatePerOwinContext<_360Appraisal.Models.ApplicationUserManager>(_360Appraisal.Models.ApplicationUserManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                CookieHttpOnly = true,
                ExpireTimeSpan = TimeSpan.FromMinutes(60),
                SlidingExpiration = true,
                CookieSecure = CookieSecureOption.SameAsRequest,
                LoginPath = new PathString("/Home/Login")
            });
        }
    }
}