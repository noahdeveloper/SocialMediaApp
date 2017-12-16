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

            string userName = Request.Cookies["userNameSocialMediaApp"].Value;

            ViewBag.UserName = userName;

            viewModel.ListOfUsers = (from x in _context.UserInformation where x.ID != userName select x).ToList();

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

            data = ImageInServer(fileName);

            return Json(new { result = data });
        }

        public string ImageInServer(string fileName)
        {
            string data = String.Empty;
            string path = Server.MapPath("~/Uploads/");

            if (System.IO.File.Exists(path + fileName + ".JPG"))
            {
                data = fileName + ".JPG";
            }
            else if (System.IO.File.Exists(path + fileName + ".JPE"))
            {
                data = fileName + ".JPE";
            }
            else if (System.IO.File.Exists(path + fileName + ".BMP"))
            {
                data = fileName + ".BMP";
            }
            else if (System.IO.File.Exists(path + fileName + ".GIF"))
            {
                data = fileName + ".GIF";
            }
            else if (System.IO.File.Exists(path + fileName + ".PNG"))
            {
                data = fileName + ".PNG";
            }
            else
            {
                data = "Not Found";
            }

            return data;
        }

    }
}