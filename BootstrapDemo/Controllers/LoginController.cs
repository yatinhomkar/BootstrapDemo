using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootstrapDemo.Models;

namespace BootstrapDemo.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(User userModel)
        {
            using (CountryReportsEntities db = new CountryReportsEntities())
            {
                var userDetails = db.Users.Where(x => x.UserEmail == userModel.UserEmail && x.Password == userModel.Password).FirstOrDefault();

                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Wrong UserEmail or Password";
                    return View("Index", userModel);
                }
                else
                {
                    Session["userID"] = userDetails.UserID;
                    Session["userName"] = userDetails.UserFirstName + " " + userDetails.UserLastName;

                    return RedirectToAction("Index", "Dashboard");
                }
            }
            // return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");

        }
    }
}