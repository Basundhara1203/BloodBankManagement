using Blood_Bank_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Blood_Bank_Management.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account

        public ActionResult Registeruser()
        {

            return View();
        }
        [HttpPost]
        public ActionResult RegisterUser(Users m)
        {
            truYumContext ds = new truYumContext();
            ds.Users.Add(m);
            ds.SaveChanges();
            ModelState.Clear();
            ViewBag.Message = "Registration Successful";
            return View("Registeruser");

        }



        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Users model)
        {
            truYumContext t = new truYumContext();
            var isvalid = t.Users.Any(x => x.UserName == model.UserName && x.Password == model.Password);
            if (isvalid)
            {
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                return RedirectToAction("Welcome", "Welcome");
            }
            else
                ModelState.AddModelError("", "Invalid name or password");
            return View();

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Dashboard","Welcome");


        }

    }
}