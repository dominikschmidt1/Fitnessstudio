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
    public class LeistungsController : BaseController
    {

        // GET: Leistungs
        public ActionResult Index()
        {
            if (!hasPerson())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(db.LeistungSet.ToList());
        }

        // GET: Leistungs/Details/5
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
            Leistung leistung = db.LeistungSet.Find(id);
            if (leistung == null)
            {
                return HttpNotFound();
            }
            return View(leistung);
        }

        // GET: Leistungs/Create
        public ActionResult Create()
        {
            if (!hasPerson())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }

        // POST: Leistungs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Bezeichnung,Preis")] Leistung leistung)
        {
            if (ModelState.IsValid)
            {
                db.LeistungSet.Add(leistung);
                db.SaveChanges();
                return RedirectToAction("Index","Mitarbeiter");
            }

            return View(leistung);
        }

        // GET: Leistungs/Edit/5
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
            Leistung leistung = db.LeistungSet.Find(id);
            if (leistung == null)
            {
                return HttpNotFound();
            }
            return View(leistung);
        }

        // POST: Leistungs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Bezeichnung,Preis")] Leistung leistung)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leistung).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(leistung);
        }

        // GET: Leistungs/Delete/5
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
            Leistung leistung = db.LeistungSet.Find(id);
            if (leistung == null)
            {
                return HttpNotFound();
            }
            return View(leistung);
        }

        // POST: Leistungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Leistung leistung = db.LeistungSet.Find(id);
            db.LeistungSet.Remove(leistung);
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
