using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InterventionWebSite.Models;

namespace InterventionWebSite.Controllers
{
    [Authorize(Roles = "Reception employee,Admin")]
    public class RequestsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Requests
        public ActionResult Index()
        {
            var requests = db.requests.Include(r => r.ReceptionEmployee).Include(r => r.Staff);
            return View(requests.ToList());
        }

        // GET: Requests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.requests.Include(r => r.ReceptionEmployee).Include(r => r.Staff).Where(a=>a.RequestId == id).First();
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // GET: Requests/Create
        public ActionResult Create()
        {
            ViewBag.ReceptionEmployeeId = new SelectList(db.users.Where(a => a.Role.RoleName == "Reception employee"), "UserId", "Firstname");
            ViewBag.StaffId = new SelectList(db.staffs, "StaffId", "LastName");
            return View();
        }

        // POST: Requests/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RequestId,TypeOfCommunication,problems,StaffId")] Request request)
        {
            if (ModelState.IsValid)
            {
                DateTime now = DateTime.Now;
                request.RequestDate = now;
                User user = db.users.Where(u => u.Email == HttpContext.User.Identity.Name).First();
                request.ReceptionEmployeeId=user.UserId;

                db.requests.Add(request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ReceptionEmployeeId = new SelectList(db.users.Where(a => a.Role.RoleName == "Reception employee"), "UserId", "Firstname", request.ReceptionEmployeeId);
            ViewBag.StaffId = new SelectList(db.staffs, "StaffId", "LastName", request.StaffId);
            return View(request);
        }

        // GET: Requests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.requests.Include(r => r.ReceptionEmployee).Include(r => r.Staff).Where(a => a.RequestId == id).First();
            if (request == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReceptionEmployeeId = new SelectList(db.users.Where(a => a.Role.RoleName == "Reception employee"), "UserId", "Firstname", request.ReceptionEmployeeId);
            ViewBag.StaffId = new SelectList(db.staffs, "StaffId", "LastName", request.StaffId);
            return View(request);
        }

        // POST: Requests/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RequestId,TypeOfCommunication,problems,StaffId")] Request request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(request).State = EntityState.Modified;
                    db.Entry(request).Property(x => x.RequestDate).IsModified = false;
                    db.Entry(request).Property(x => x.ReceptionEmployeeId).IsModified = false;
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return RedirectToAction("Index");
            }
            ViewBag.ReceptionEmployeeId = new SelectList(db.users.Where(a => a.Role.RoleName == "Reception employee"), "UserId", "Firstname", request.ReceptionEmployeeId);
            ViewBag.StaffId = new SelectList(db.staffs, "StaffId", "LastName", request.StaffId);
            return View(request);
        }

        // GET: Requests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.requests.Include(r => r.ReceptionEmployee).Include(r => r.Staff).Where(a => a.RequestId == id).First();
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Request request = db.requests.Find(id);
            db.requests.Remove(request);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
