using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialMediaApp.Models;
using System.IO;
using System.Web.Services;

namespace SocialMediaApp.Controllers
{
    public class UsersController : Controller
    {
        ApplicationDbContext _context;

        public UsersController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Users()
        {
            //List of all users
            UsersViewModel viewModel = new UsersViewModel();

            viewModel.ListOfUsers = (from x in _context.UserInformation select x).ToList();

            if (Request.Cookies["userNameSocialMediaApp"].Value != null)
            {
                ViewBag.UserName = Request.Cookies["userNameSocialMediaApp"].Value;
            }
            else
            {
                return RedirectToAction("SignIn", "SignIn");
            }

            return View(viewModel);
        }

        public ActionResult GoToUsersHome(string userName)
        {
            UserInformation model = new UserInformation();

            var user = _context.UserInformation.Where(x => x.ID == userName).FirstOrDefault();

            model.ID = user.ID;
            model.Age = user.Age;
            model.FavoriteQuote = user.FavoriteQuote;
            model.ProfilePicName = user.ProfilePicName;

            ViewBag.UserName = Request.Cookies["userNameSocialMediaApp"].Value;

            return View(model);
        }

        //Gets image with parameter name
        [WebMethod]
        public ActionResult GetImageName(string fileName)
        {
            string data = String.Empty;
            string path = Server.MapPath("~/Uploads/");

            if (!String.IsNullOrEmpty(fileName))
            {
                if (System.IO.File.Exists(path + fileName))
                {
                    data = Path.GetFileName(path + fileName);
                }
            }
            else
            {
                data = "No data found.";
            }


            return Json(new { foo = "123" });
        }

        //Function to check if file exists in server:
        //Create function to check for file where it starts with user name.

    }
}