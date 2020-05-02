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
    [Authorize(Roles = "Admin,Reception employee")]
    public class StaffsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Staffs
        public ActionResult Index()
        {
            var staffs = db.staffs.Include(s => s.Agency).Include(s => s.Direction);
            return View(staffs.ToList());
        }

        // GET: Staffs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // GET: Staffs/Create
        public ActionResult Create()
        {
            ViewBag.AgencyId = new SelectList(db.agencies, "AgencyId", "AgencyName");
            ViewBag.DirectionId = new SelectList(db.directions, "DirectionId", "DirectionName");
            return View();
        }

        // POST: Staffs/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StaffId,LastName,FirstName,Email,Phone,AgencyId,DirectionId")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                db.staffs.Add(staff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AgencyId = new SelectList(db.agencies, "AgencyId", "AgencyName", staff.AgencyId);
            ViewBag.DirectionId = new SelectList(db.directions, "DirectionId", "DirectionName", staff.DirectionId);
            return View(staff);
        }

        // GET: Staffs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgencyId = new SelectList(db.agencies, "AgencyId", "AgencyName", staff.AgencyId);
            ViewBag.DirectionId = new SelectList(db.directions, "DirectionId", "DirectionName", staff.DirectionId);
            return View(staff);
        }

        // POST: Staffs/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StaffId,LastName,FirstName,Email,Phone,AgencyId,DirectionId")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AgencyId = new SelectList(db.agencies, "AgencyId", "AgencyName", staff.AgencyId);
            ViewBag.DirectionId = new SelectList(db.directions, "DirectionId", "DirectionName", staff.DirectionId);
            return View(staff);
        }

        // GET: Staffs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Staff staff = db.staffs.Find(id);

            foreach (var item in staff.Requests.ToList())
            {
                db.requests.Remove(item);
            }
            
            db.staffs.Remove(staff);
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
