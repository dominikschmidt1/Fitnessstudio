using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FitnessstudioLib;

namespace FitnessstudioWebApp.Controllers
{
    public class PeopleController : BaseController
    {

        // GET: People
        public ActionResult Index()
        {
            if (!hasPerson())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(db.PersonSet.ToList());
        }

        // GET: People/Details/5
        public ActionResult Details(int? id)
        {
            if (!hasPerson())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.PersonSet.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            if (!hasPerson())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nachname,Vorname,Wohnort,Bank,Email,RoleStaff,RoleMember")] Person person)
        {
            if (person.RoleStaff == false && person.RoleMember == false)
            {
                ModelState.AddModelError("", "Person muss entweder Mitglied oder Mitarbeiter sein");
                return View(person);
            }
            if (person.RoleStaff == true && person.RoleMember == true)
            {
                ModelState.AddModelError("", "Person darf nur Mitglied oder Mitarbeiter sein");
                return View(person);
            }
            if (ModelState.IsValid)
            {
                db.PersonSet.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index", "Mitarbeiter");
            }

            return View(person);
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
