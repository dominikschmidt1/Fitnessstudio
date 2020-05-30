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
    public class MitarbeiterController : BaseController
    {

        // GET: Mitarbeiter
        public ActionResult Index()
        {
            return View();
        }

        // GET: Mitarbeiter
        public ActionResult Profil()
        {
            if (!hasPerson())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Person> mitarbeiter = new List<Person>();
            mitarbeiter.Add(EingeloggtePerson);
            return View(mitarbeiter);
        }

        // GET: Mitarbeiter/Details/5
        public ActionResult Details(int? id)
        {
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

        // GET: Mitarbeiter/Edit/5
        public ActionResult Edit(int? id)
        {
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

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nachname,Vorname,Wohnort,Bank,Email,RoleStaff,RoleMember")] Person person)
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
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Profil", "Mitarbeiter");
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
