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
            if (!hasPerson())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
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

        // GET: Mitarbeiter/Edit/5
        public ActionResult Edit(int? id)
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
                return RedirectToAction("Index", "Mitarbeiter");
            }
            return View(person);
        }

        // GET: Mitarbeiter/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.PersonSet.Find(id);
            db.PersonSet.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SelectEdit()
        {
            if (!hasPerson())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(db.PersonSet.Where(m => m.Id != EingeloggtePerson.Id).ToList());
            // return View(db.PersonSet.Where(u => u.RoleMember).ToList());
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
