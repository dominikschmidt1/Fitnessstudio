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
    public class KatalogController : BaseController
    {

        // GET: Katalog
        public ActionResult Index()
        {
            if (!hasPerson())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.HinzugefuegteIds = EingeloggtePerson.VerfuegtUeber.Select(o => o.Id).ToList();
            return View(db.LeistungSet.ToList());
        }

        // GET: Katalog/Hinzufügen/5
        public ActionResult Add(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (!hasPerson())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leistung leistung = db.LeistungSet.Find(id);
            if (leistung == null)
            {
                return HttpNotFound();
            }
            EingeloggtePerson.VerfuegtUeber.Add(leistung);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Katalog/Abwählen/5
        public ActionResult Deselect(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (!hasPerson())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leistung leistung = db.LeistungSet.Find(id);
            if (leistung == null)
            {
                return HttpNotFound();
            }
            EingeloggtePerson.VerfuegtUeber.Remove(leistung);
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