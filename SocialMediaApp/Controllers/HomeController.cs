using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialMediaApp.Models;
using System.Web.Services;

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
                UserInformation model = new UserInformation();

                var user = _context.UserInformation.Where(x => x.ID == userName).FirstOrDefault();

                model.ID = user.ID;
                model.Age = user.Age;
                model.FavoriteQuote = user.FavoriteQuote;
                model.ProfilePicName = user.ProfilePicName;

                if (user.Age == 0 && user.FavoriteQuote == null && user.ProfilePicName == null)
                {
                    ViewBag.UserName = userName;
                    ViewBag.NoInfo = true;
                    return View(model);
                }

                ViewBag.UserName = userName;
                return View(model);
            }

            return RedirectToAction("SignIn", "SignIn");
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