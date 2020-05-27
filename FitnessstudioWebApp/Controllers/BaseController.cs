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
    public class BaseController : Controller
    {
        protected FitnessstudioModelContainer db = new FitnessstudioModelContainer();

        private static String PersonIdKey = "PersonId";
        public Person setPerson()
        {
            int? personId = Session[PersonIdKey] as int?;
            if (personId == null) return null;
            Person res = db.PersonSet.Find(personId);
            if (res == null) return null;
            ViewBag.Person = res;
            _EingeloggtePerson = res;
            return res;
        }

        private Person _EingeloggtePerson = null;
        protected Person EingeloggtePerson => _EingeloggtePerson ?? setPerson();

        protected bool hasPerson()
        {
            return EingeloggtePerson != null;
        }

        protected void setPersonId(int personId)
        {
            Session[PersonIdKey] = personId;
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
