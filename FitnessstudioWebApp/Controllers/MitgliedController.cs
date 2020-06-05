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
    public class MitgliedController : BaseController
    {

        // GET: Mitglied
        public ActionResult Index()
        {
            if (!hasPerson())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Person> mitglied = new List<Person>();
            mitglied.Add(EingeloggtePerson);
            var person = db.PersonSet.Where(m => m.Id == EingeloggtePerson.Id).Include(m => m.VerfuegtUeber);
            return View(person.ToList());
        }

        // GET: Mitglied/Details/5
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

        // GET: Mitglied/Edit/5
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

        // POST: Mitglied/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nachname,Vorname,Wohnort,Bank,Email")] Person person)
        {
            if (!hasPerson())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                Person personOriginal = db.PersonSet.Find(person.Id);
                personOriginal.Id = person.Id;
                personOriginal.Nachname = person.Nachname;
                personOriginal.Vorname = person.Vorname;
                personOriginal.Wohnort = person.Wohnort;
                personOriginal.Bank = person.Bank;
                personOriginal.Email = person.Email;
                db.SaveChanges();
                return RedirectToAction("Index");
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
