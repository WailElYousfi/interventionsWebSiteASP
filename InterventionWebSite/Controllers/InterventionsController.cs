using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InterventionWebSite.Models;
using Newtonsoft.Json;

namespace InterventionWebSite.Controllers
{
    [Authorize]
    public class InterventionsController : Controller
    {
        private DataContext db = new DataContext();
    
        // GET: Interventions
        public ActionResult Index()
        {
            var interventions = db.interventions.Include(i => i.Agency).Include(i => i.AssignmentManager).Include(i => i.Direction).Include(i => i.Intervener).Include(i => i.Request).Include(i => i.State);
            return View(interventions.ToList());
        }

        // GET: Interventions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Intervention intervention = db.interventions.Include(i => i.Agency).Include(i => i.AssignmentManager).Include(i => i.Direction).Include(i => i.Intervener).Include(i => i.Request).Include(i => i.State).Where(i=>i.InterventionId == id).First();
            if (intervention == null)
            {
                return HttpNotFound();
            }
            return View(intervention);
        }

        // GET: Interventions/Create
        [Authorize(Roles = "Admin,Reception employee")]
        public ActionResult Create()
        {
            ViewBag.AgencyId = new SelectList(db.agencies, "AgencyId", "AgencyName");
            ViewBag.AssignmentManagerId = new SelectList(db.users.Where(a => a.Role.RoleName == "Assignment manager"), "UserId", "Firstname");
            ViewBag.DirectionId = new SelectList(db.directions, "DirectionId", "DirectionName");
            ViewBag.IntervenerId = new SelectList(db.users.Where(a => a.Role.RoleName == "Intervener"), "UserId", "Firstname");
            ViewBag.RequestId = new SelectList(db.requests, "RequestId", "RequestId");
            ViewBag.StateId = new SelectList(db.states, "StateId", "StateName");
            return View();
        }

        public JsonResult GetRequestDetails(int RequestId)
        {

            Request request = db.requests.Include(r => r.ReceptionEmployee).Include(r => r.Staff).Include(r => r.Staff.Agency).Include(r => r.Staff.Direction).Where(a => a.RequestId == RequestId).First();
            return Json(new {  requestId=request.RequestId,
                problem=request.problems,
                agencyName =request.Staff.Agency.AgencyName,
                directionName =request.Staff.Direction.DirectionName,
                address=request.Staff.Agency.Address}, JsonRequestBehavior.AllowGet);
        }

        // POST: Interventions/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InterventionId,Nature,Description,AgencyId,DirectionId,RequestId")] Intervention intervention)
        {
            if (ModelState.IsValid)
            {
                intervention.StateId = db.states.Where(s => s.StateName == "A affecter").First().StateId;
                Request request = db.requests.Find(intervention.RequestId);
                Staff staff = db.staffs.Find(request.StaffId);
                Agency agency = db.agencies.Find(staff.AgencyId);
                Direction direction = db.directions.Find(staff.DirectionId);

                intervention.AgencyId = agency.AgencyId;
                intervention.DirectionId = direction.DirectionId;
                intervention.ActionDate= null;
                intervention.ClosingDate = null;
                intervention.IntervenerId = null;
                intervention.AssignmentManagerId = null;

                db.interventions.Add(intervention);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AgencyId = new SelectList(db.agencies, "AgencyId", "AgencyName", intervention.AgencyId);
            ViewBag.AssignmentManagerId = new SelectList(db.users.Where(a => a.Role.RoleName == "Assignment manager"), "UserId", "Firstname", intervention.AssignmentManagerId);
            ViewBag.DirectionId = new SelectList(db.directions, "DirectionId", "DirectionName", intervention.DirectionId);
            ViewBag.IntervenerId = new SelectList(db.users.Where(a => a.Role.RoleName == "Intervener"), "UserId", "Firstname", intervention.IntervenerId);
            ViewBag.RequestId = new SelectList(db.requests, "RequestId", "TypeOfCommunication", intervention.RequestId);
            ViewBag.StateId = new SelectList(db.states, "StateId", "StateName", intervention.StateId);
            return View(intervention);
        }

        // GET: Interventions/Edit/5
        [Authorize(Roles = "Admin,Reception employee,Assignment manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Intervention intervention = db.interventions.Include(i => i.Agency).Include(i => i.AssignmentManager).Include(i => i.Direction).Include(i => i.Intervener).Include(i => i.Request).Include(i => i.State).Where(i => i.InterventionId == id).First();
            if (intervention == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgencyId = new SelectList(db.agencies, "AgencyId", "AgencyName", intervention.AgencyId);
            ViewBag.AssignmentManagerId = new SelectList(db.users.Where(a => a.Role.RoleName == "Assignment manager"), "UserId", "Firstname", intervention.AssignmentManagerId);
            ViewBag.DirectionId = new SelectList(db.directions, "DirectionId", "DirectionName", intervention.DirectionId);
            ViewBag.IntervenerId = new SelectList(db.users.Where(a => a.Role.RoleName == "Intervener"), "UserId", "Firstname", intervention.IntervenerId);
            ViewBag.RequestId = new SelectList(db.requests, "RequestId", "RequestId", intervention.RequestId);
            ViewBag.StateId = new SelectList(db.states, "StateId", "StateName", intervention.StateId);
            return View(intervention);
        }

        // POST: Interventions/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InterventionId,Nature,Description,RequestId")] Intervention intervention)
        {
            if (ModelState.IsValid)
            {
                db.Entry(intervention).State = EntityState.Modified;
                db.Entry(intervention).Property(x => x.StateId).IsModified = false; //  Pour ne pas changer l'ancienne valeur
                db.Entry(intervention).Property(x => x.ActionDate).IsModified = false;
                db.Entry(intervention).Property(x => x.ClosingDate).IsModified = false;
                db.Entry(intervention).Property(x => x.IntervenerId).IsModified = false;
                db.Entry(intervention).Property(x => x.AssignmentManagerId).IsModified = false;

               
                Request request = db.requests.Find(intervention.RequestId);
                Staff staff = db.staffs.Find(request.StaffId);
                Agency agency = db.agencies.Find(staff.AgencyId); // Pour recuperer automatiquement l'agence du personnel qui a fait la demande
                Direction direction = db.directions.Find(staff.DirectionId);

                intervention.AgencyId = agency.AgencyId;
                intervention.DirectionId = direction.DirectionId;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AgencyId = new SelectList(db.agencies, "AgencyId", "AgencyName", intervention.AgencyId);
            ViewBag.AssignmentManagerId = new SelectList(db.users.Where(a => a.Role.RoleName == "Assignment manager"), "UserId", "Firstname", intervention.AssignmentManagerId);
            ViewBag.DirectionId = new SelectList(db.directions, "DirectionId", "DirectionName", intervention.DirectionId);
            ViewBag.IntervenerId = new SelectList(db.users.Where(a => a.Role.RoleName == "Intervener"), "UserId", "Firstname", intervention.IntervenerId);
            ViewBag.RequestId = new SelectList(db.requests, "RequestId", "TypeOfCommunication", intervention.RequestId);
            ViewBag.StateId = new SelectList(db.states, "StateId", "StateName", intervention.StateId);
            return View(intervention);
        }

        // GET: Interventions/Delete/5
        [Authorize(Roles = "Admin,Reception employee")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Intervention intervention = db.interventions.Include(i => i.Agency).Include(i => i.AssignmentManager).Include(i => i.Direction).Include(i => i.Intervener).Include(i => i.Request).Include(i => i.State).Where(i => i.InterventionId == id).First();
            if (intervention == null)
            {
                return HttpNotFound();
            }
            return View(intervention);
        }

        // POST: Interventions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Intervention intervention = db.interventions.Find(id);
            db.interventions.Remove(intervention);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: Interventions/Assign/5
        [Authorize(Roles = "Admin,Assignment manager")]
        public ActionResult Assign(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Intervention intervention = db.interventions.Include(i => i.Agency).Include(i => i.AssignmentManager).Include(i => i.Direction).Include(i => i.Intervener).Include(i => i.Request).Include(i => i.State).Where(i => i.InterventionId == id).First();
            if (intervention == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgencyId = new SelectList(db.agencies, "AgencyId", "AgencyName", intervention.AgencyId);
            ViewBag.AssignmentManagerId = new SelectList(db.users.Where(a => a.Role.RoleName == "Assignment manager"), "UserId", "Firstname", intervention.AssignmentManagerId);
            ViewBag.DirectionId = new SelectList(db.directions, "DirectionId", "DirectionName", intervention.DirectionId);
            ViewBag.IntervenerId = new SelectList(db.users.Where(a => a.Role.RoleName == "Intervener"), "UserId", "Firstname", intervention.IntervenerId);
            ViewBag.RequestId = new SelectList(db.requests, "RequestId", "RequestId", intervention.RequestId);
            ViewBag.StateId = new SelectList(db.states, "StateId", "StateName", intervention.StateId);
            return View(intervention);
        }

        // POST: Interventions/Assign/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Assign([Bind(Include = "InterventionId,IntervenerId")] Intervention intervention)
        {
            ModelState.Remove("Nature"); // pour ne pas verifier la validation(required)
            ModelState.Remove("Description");

            if (ModelState.IsValid)
            {
                Intervention inter = db.interventions.Find(intervention.InterventionId);
                inter.IntervenerId = intervention.IntervenerId;
                User currentUser = db.users.Where(u => u.Email == HttpContext.User.Identity.Name).First();
                inter.AssignmentManagerId = currentUser.UserId;
                inter.StateId = db.states.Where(s => s.StateName == "En cours").First().StateId;
                DateTime now = DateTime.Now;
                inter.ActionDate = now;
                db.Entry(inter).State = EntityState.Modified;
              
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AgencyId = new SelectList(db.agencies, "AgencyId", "AgencyName", intervention.AgencyId);
            ViewBag.AssignmentManagerId = new SelectList(db.users.Where(a => a.Role.RoleName == "Assignment manager"), "UserId", "Firstname", intervention.AssignmentManagerId);
            ViewBag.DirectionId = new SelectList(db.directions, "DirectionId", "DirectionName", intervention.DirectionId);
            ViewBag.IntervenerId = new SelectList(db.users.Where(a => a.Role.RoleName == "Intervener"), "UserId", "Firstname", intervention.IntervenerId);
            ViewBag.RequestId = new SelectList(db.requests, "RequestId", "TypeOfCommunication", intervention.RequestId);
            ViewBag.StateId = new SelectList(db.states, "StateId", "StateName", intervention.StateId);
            return View(intervention);
        }

        [Authorize(Roles = "Admin,Assignment manager")]
        public ActionResult Close(int? id)
        {
            Intervention intervenion = db.interventions.Find(id);
            intervenion.StateId = db.states.Where(s => s.StateName == "Cloturee").First().StateId;
            DateTime now = DateTime.Now;
            intervenion.ClosingDate = now;
            db.Entry(intervenion).State = EntityState.Modified;

            db.SaveChanges();
            return RedirectToAction("Index", "Interventions"); // index is the action name, interventions is the controller name.
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
