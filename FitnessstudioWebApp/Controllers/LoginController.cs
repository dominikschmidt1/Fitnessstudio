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
    public class LoginController : BaseController
    {

        // GET: Login
        public ActionResult Index()
        {
            return View(db.PersonSet.ToList());
        }

        // GET: Login/Details/5
        public ActionResult Select(int? id)
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
            setPersonId(id.Value);
            if (EingeloggtePerson.RoleMember == true)
            {
                return RedirectToAction("Index","Mitglied");
            }
            if (EingeloggtePerson.RoleStaff == true)
            {
                return RedirectToAction("Index","Mitarbeiter");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
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
