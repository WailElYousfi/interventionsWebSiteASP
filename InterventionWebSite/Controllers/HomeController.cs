using InterventionWebSite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterventionWebSite.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private DataContext db = new DataContext();

        public ActionResult Index()
        {
            if (HttpContext.User.IsInRole("Admin"))
            {
                List<Intervention> list1 = db.interventions.Include(i => i.State).ToList();
                List<Request> list2 = db.requests.ToList();
                var model = new ViewModel { InterventionsTable = list1, RequestsTable = list2 };
                return View(model);
            } else if(HttpContext.User.IsInRole("Assignment manager") || HttpContext.User.IsInRole("Intervener"))
                return RedirectToAction("Index", "Interventions");
            else //Reception employee
                return RedirectToAction("Index", "Requests");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}