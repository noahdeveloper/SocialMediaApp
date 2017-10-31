using SocialMediaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialMediaApp.Controllers
{
    public class SignInController : Controller
    {
        public ApplicationDbContext _context;

        public SignInController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult SignIn()
        {
            SignInViewModel model = new SignInViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult SignIn(SignInViewModel model)
        {
            //Check if userName is in db.
            bool exists = UserNameInDB(model.UserName);

            //Check if correct password for userName.
            if (exists)
            {
                bool correctPassword = CorrectPassWord(model.UserName, model.Password);

                if (correctPassword)
                {
                    return RedirectToAction("Index", "Home", new { userName = model.UserName });
                }
                else
                {
                    ViewBag.IncorrectPassword = true;
                    return View();
                }
            }
            else
            {
                ViewBag.UserDoesNotExist = true;
                return View();
            }
        }

        public bool UserNameInDB(string userName)
        {
            bool result = true;

            var user = _context.UserAccount.Where(x => x.UserName == userName).Select(x => x.UserName).FirstOrDefault();

            if (String.IsNullOrEmpty(user))
                return false;

            return result;
        }

        public bool CorrectPassWord(string userName, string password)
        {
            bool result = true;

            var DBPassword = _context.UserAccount.Where(x => x.UserName == userName).Select(x => x.Password).FirstOrDefault();

            if (!String.Equals(password, DBPassword))
                result = false;

            return result;
        }
    }
}