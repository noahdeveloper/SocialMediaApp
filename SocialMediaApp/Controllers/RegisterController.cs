using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialMediaApp.Models;

namespace SocialMediaApp.Controllers
{
    public class RegisterController : Controller
    {
        public ApplicationDbContext _context;

        public RegisterController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult SignUp()
        {
            UserAccount model = new UserAccount();
            //get next user id available
            return View(model);
        }

        [HttpPost]
        public ActionResult SignUp(UserAccount model)
        {
            //model state valid?
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //does password register good?
            if (!PasswordConfim(model.Password, model.ConfimPassword))
            {
                ViewBag.PasswordNotConfirmed = true;
                return View();
            }

            //does username exist?
            if (DoesUsernameAlreadyExist(model.UserName))
            {
                ViewBag.UserNameExists = true;
                return View();
            }

            //does user fit in DB? no more than 13 allowed, i don't want to get charged.
            if (!DoesUserFitInDB())
            {
                ViewBag.UserDoesNotFit = true;
                return View();
            }

            model.IPAddressCreatedAccountDevice = Request.UserHostAddress;

            UserInformation userInfoModel = new UserInformation
            {
                ID = model.UserName
            };

            try
            {
                _context.UserAccount.Add(model);

                _context.UserInformation.Add(userInfoModel);

                _context.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            return RedirectToAction("Index", "Home", new { model.UserName });
        }

        public bool PasswordConfim(string password, string passwordConfirm)
        {
            bool result = false;

            if (String.Equals(password, passwordConfirm))
            {
                result = true;
                return result;
            }

            return result;
        }

        public bool DoesUsernameAlreadyExist(string userName)
        {
            bool result = true;

            string nameInDB = _context.UserAccount.Where(x => x.UserName == userName).Select(x => x.UserName).FirstOrDefault();

            if (String.IsNullOrEmpty(nameInDB))
            {
                result = false;
                return result;
            }

            return result;
        }

        public bool DoesUserFitInDB()
        {
            bool result = true;

            List<UserAccount> listOfUsers = _context.UserAccount.ToList();

            if (listOfUsers.Count() >= 12)
            {
                result = false;
                return result;
            }

            return result;
        }
    }
}