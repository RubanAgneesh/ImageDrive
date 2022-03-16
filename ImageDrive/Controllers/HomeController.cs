using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageDrive.Models;

namespace ImageDrive.Controllers
{
    public class HomeController : Controller
    {
        DBmodel db = new DBmodel();
        //GET: Home
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        //public ActionResult Signup(User user)
        //{
        //    if (db.Users.Any(x => x.Email == user.Email))
        //    {
        //        ViewBag.Notification = "This account is already existed";
        //        return View();
        //    }
        //    else
        //    {
        //        db.Users.Add(user);
        //        db.SaveChanges();

        //        Session["IdUsSS"] = user.Id.ToString();
        //        Session["Email"] = user.Email.ToString();
        //        return RedirectToAction("Index", "Home");
        //    }
        //}
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(User user)
        {
            var checkLogin = db.Users.Where(x => x.Email.Equals(user.Email) && x.Password.Equals(user.Password)).FirstOrDefault();
            if (checkLogin != null)
            {
                Session["IdUsSS"] = user.Id.ToString();
                Session["Email"] = user.Email.ToString();
                return RedirectToAction("Create", "TestImage");
                //return RedirectToAction("Home", "Create");
            }
            else
            {
                ViewBag.Notification = "wrong username or password";
            }
            return View();
        }
    }
}