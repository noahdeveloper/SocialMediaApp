using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialMediaApp.Models;

namespace SocialMediaApp.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index(string userName)
        {
            if (!String.IsNullOrEmpty(userName))
            {
                ViewBag.UserName = userName;
                return View();
            }

            return RedirectToAction("SignIn", "SignIn");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}