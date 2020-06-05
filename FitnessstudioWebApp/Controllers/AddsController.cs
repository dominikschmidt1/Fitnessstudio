using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FitnessstudioLib;
using FitnessstudioWebApp.Models;

namespace FitnessstudioWebApp.Controllers
{
    public class AddsController : BaseController
    {

        private class AddOrError
        {
            public ActionResult Error;
            public Add Add;
            public AddOrError (ActionResult error)
            {
                Error = error;
            }
            public AddOrError(HttpStatusCode errorStatus)
            {
                Error = new HttpStatusCodeResult(errorStatus);
            }
            public AddOrError(Add add)
            {
                Add = add;
            }
        }
        private AddOrError GetAddOrError(int? personId, int? leistungId)
        {
            if (!hasPerson())
                return new AddOrError(HttpStatusCode.Unauthorized);
            if (personId == null)
                return new AddOrError(HttpStatusCode.BadRequest);
            if (leistungId == null)
                return new AddOrError(HttpStatusCode.BadRequest);
            Person person = db.PersonSet.Find(personId);
            if (person == null)
                return new AddOrError(HttpStatusCode.BadRequest);
            Leistung leistung = db.LeistungSet.Find(leistungId);
            if (leistung == null)
                return new AddOrError(HttpStatusCode.BadRequest);
            return new AddOrError(new Add() { Person = person, Leistung = leistung });

        }

        // GET: Adds
        public ActionResult Index()
        {
            if (!hasPerson())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var adds =
                from person in db.PersonSet
                from leistung in db.LeistungSet
                where person.VerfuegtUeber.Contains(leistung)
                select new Add()
                    { Person = person, Leistung = leistung };
            return View(adds.ToList());
        }

        // GET: Adds/Details/5
        public ActionResult Details(int? personId, int? leistungId)
        {
            var tmp = GetAddOrError(personId, leistungId);
            if (tmp.Error != null) return tmp.Error;
            return View(tmp.Add);
        }

        // GET: Adds/Create
        public ActionResult Create(int? personId, int? leistungId)
        {
            if (!hasPerson())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (personId != null && leistungId != null)
                return Create(
                    new Add {
                        PersonId = (int)personId,
                        LeistungId = (int)leistungId });
            ViewBag.PersonId = new SelectList(db.PersonSet, "Id", "Nachname");
            ViewBag.LeistungId = new SelectList(db.LeistungSet, "Id", "Bezeichnung");
            return View();
        }

        // POST: Adds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PersonId,LeistungId")] Add add)
        {
            var tmp = GetAddOrError(add.PersonId, add.LeistungId);
            if (tmp.Error != null) return tmp.Error;
            if (ModelState.IsValid)
            {
                tmp.Add.Person.VerfuegtUeber.Add(tmp.Add.Leistung);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonId = new SelectList(db.PersonSet, "Id", "Nachname", add.PersonId);
            ViewBag.LeistungId = new SelectList(db.LeistungSet, "Id", "Bezeichnung", add.LeistungId);
            return View(add);
        }

        // GET: Adds/Delete/5
        public ActionResult Delete(int? personId, int? leistungId )
        {
            var tmp = GetAddOrError(personId, leistungId);
            if (tmp.Error != null) return tmp.Error;
            return View(tmp.Add);
        }

        // POST: Adds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? personId, int? leistungId)
        {
            var tmp = GetAddOrError(personId, leistungId);
            if (tmp.Error != null) return tmp.Error;
            tmp.Add.Person.VerfuegtUeber.Remove(tmp.Add.Leistung);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Adds/SelectPerson/5
        public ActionResult SelectPerson(int? leistungId)
        {
            if (leistungId != null)
            {
                var leistung = db.LeistungSet.Find(leistungId);
                if (leistung == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                ViewBag.Leistung = leistung;
            }
            return View(db.PersonSet.Where(u => u.RoleMember).ToList());
        }

        // GET: Adds/SelectLeistung/5
        public ActionResult Selectleistung(int? personId)
        {
            if (personId != null)
            {
                var person = db.PersonSet.Find(personId);
                if (person == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                ViewBag.Member = person;
            }
            return View(db.LeistungSet.ToList());
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
