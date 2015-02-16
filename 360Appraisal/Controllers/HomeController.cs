using _360Appraisal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security;
using System.Web.Mvc;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Security.Claims;

namespace _360Appraisal.Controllers
{
    [OutputCache(NoStore = true, Location = OutputCacheLocation.None, Duration = 0)]
    public class HomeController : Controller
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                var identity = (ClaimsIdentity)User.Identity;
                ViewBag.Username = identity.GetUserName();
                return View();
            }

            return View("Login");
        }

        [HttpGet]
        public ActionResult Login(string ReturnUrl = "")
        {
            // If the user IsAuthenticated return to Index action, skip login only if the ReturnUrl IsNullOrWhiteSpace.
            if (Request.IsAuthenticated)
            {
                if (String.IsNullOrWhiteSpace(ReturnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(new LoginViewModel()
            {
                // Assign ReturnUrl to Model
                _ReturnUrl = HttpUtility.UrlEncode(ReturnUrl)
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login([Bind(Include = "UserName, Password, RememberMe, _ReturnUrl")]LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Id = "ca94",
                    UserName = "Prasanth"
                };

                if (user != null)
                {
                    await UserManager.SignInAsync(user, model.RememberMe);

                    return RedirectToAction("Index", "Home");
                }
            }

            return HttpNotFound();
        }
    }
}
