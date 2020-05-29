﻿using System;
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
            return View(db.PersonSet.ToList());
        }

        // GET: People/Details/5
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

        // GET: People/Create
        public ActionResult Create()
        {
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
                return RedirectToAction("Index");
            }

            return View(person);
        }

        // GET: People/Edit/5
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
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: People/Delete/5
        public ActionResult Delete(int? id)
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
