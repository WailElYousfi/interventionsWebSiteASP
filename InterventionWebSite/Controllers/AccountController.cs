using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using InterventionWebSite.Models;

namespace InterventionWebSite.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private DataContext db = new DataContext();

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View(new LoginModel());
            }else
                return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            // For simplicity, this example uses forms authentication with credentials stored in web.config.
            // Your application can use any authentication method you choose (eg Active Directory, custom database etc).
            // There are no restrictions on the method of authentication.
            User user = db.users.Where(u => u.Email == model.UserName).SingleOrDefault();
            if (ModelState.IsValid && user!=null && user.Password==model.Password)
            {
                FormsAuthentication.SetAuthCookie(model.UserName, false);// false :cad que l’authentiﬁcation n’aura qu’une duree de vie limitee a la session
                return RedirectToLocal(returnUrl);
            }

            ModelState.AddModelError("", "Informations invalides.");
            return View(model);
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("~/Account/Login");
        }

        public ActionResult Profil()
        {
            User user = db.users.Include(u=>u.Role).Include(u=>u.Requests).Include(u=>u.InterventionsAssignment).Include(u => u.InterventionssIntervener).Where(u=>u.Email == HttpContext.User.Identity.Name).FirstOrDefault();
            if (user != null)
            {
                return View(user);
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Account/Edit
        public ActionResult Edit()
        {
            User user = db.users.Include(u => u.Role).Include(u => u.Requests).Include(u => u.InterventionsAssignment).Include(u => u.InterventionssIntervener).Where(u => u.Email == HttpContext.User.Identity.Name).FirstOrDefault();
            if (user != null)
            {
                return View(user);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Account/Edit
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Firstname,Lastname,Email,Phone")] User user)
        {
            ModelState.Remove("Password");

            if (ModelState.IsValid)
            {
                User us = db.users.Find(user.UserId);
                us.Firstname = user.Firstname;
                us.Lastname = user.Lastname;
                us.Email = user.Email;
                us.Phone = user.Phone;
                FormsAuthentication.SetAuthCookie(user.Email, false);// false :cad que l’authentiﬁcation n’aura qu’une duree de vie limitee a la session
                db.Entry(us).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Profil");
            }
            return View(user);
        }


    }
}
