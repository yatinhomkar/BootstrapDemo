using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootstrapDemo.Models;

namespace BootstrapDemo.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult GetCountries(string term)
        {
            CountryReportsEntities1 db = new CountryReportsEntities1();

            List<string> countries;

            countries = db.CountryInfoes.Where(x => x.CountryName.StartsWith(term))
                       .Select(y => y.CountryName).ToList();

            return Json(countries, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetFavoriteCountriesData(int userID)
        {
            CountryReportsEntities2 db = new CountryReportsEntities2();
            var fav_countries = db.FavCountryInfoes.Where(x => x.UserID == userID).ToList();
            return Json(fav_countries, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public bool Delete(int FavCountryID)
        {
            CountryReportsEntities2 db = new CountryReportsEntities2();
            var country_to_delete = db.FavCountryInfoes.Find(FavCountryID);
            db.FavCountryInfoes.Remove(country_to_delete);
            db.SaveChanges();
            return true;   //RedirectToAction("Index");
        }

        [HttpPost]
        public bool Insert(FavCountryInfo favCountryInfo)
        {
            using (CountryReportsEntities2 db = new CountryReportsEntities2())
            {
                db.FavCountryInfoes.Add(favCountryInfo);
                db.SaveChanges();

            }
            return true;
        }
    }
}