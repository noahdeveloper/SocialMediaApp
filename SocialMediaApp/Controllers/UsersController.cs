using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialMediaApp.Models;
using System.IO;

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

            ViewBag.UserName = Request.Cookies["userNameSocialMediaApp"].Value;

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
        public string GetImageName(string fileName)
        {
            string file = String.Empty;
            string path = Server.MapPath("~/Uploads/");

            if (!String.IsNullOrEmpty(fileName))
            {
                if (System.IO.File.Exists(path + fileName))
                {
                    file = Path.GetFileName(path + fileName);
                }
            }
            else
            {
                file = "No file found.";
            }

            return file;
        }
    }
}