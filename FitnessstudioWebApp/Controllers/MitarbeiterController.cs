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

        // GET: Mitarbeiter/Select/5
        public ActionResult Select()
        {
            return View(db.PersonSet.ToList());
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
