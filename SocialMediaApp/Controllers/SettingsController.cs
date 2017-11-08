using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using SocialMediaApp.Models;
using System.Collections.Generic;
using System.Linq;
using SocialMediaApp.Controllers;

namespace SocialMediaApp.Controllers
{
    public class SettingsController : Controller
    {
        public ApplicationDbContext _context;

        public SettingsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Settings
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserProfile()
        {
            if (Request.Cookies["userNameSocialMediaApp"] != null)
            {
                string userNameCookie = Request.Cookies["userNameSocialMediaApp"].Value;

                UserInformation model = new UserInformation
                {
                    ID = userNameCookie
                };

                return View(model);
            }
            else
            {
                return RedirectToAction("SignIn", "SignIn");
            } 
        }

        [HttpPost]
        public ActionResult UserProfile(HttpPostedFileBase file, UserInformation model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (file != null)
            {
                string path = Server.MapPath("~/Uploads/");
                List<string> imageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };

                //if is image file
                if (imageExtensions.Contains(Path.GetExtension(Path.GetExtension(file.FileName).ToUpperInvariant())))
                {
                    try
                    {
                        Directory.CreateDirectory(path);
                        file.SaveAs(path + model.ID + Path.GetExtension(file.FileName));
                    }
                    catch (Exception ex)
                    {
                        string error = ex.Message;
                    }
                }
                else
                {
                    return View(model);
                }
            }

            model.ProfilePicName = model.ID;

            var accountInfo = _context.UserInformation.Where(x => x.ID == model.ID).FirstOrDefault();

            accountInfo.Age = model.Age;
            accountInfo.FavoriteQuote = model.FavoriteQuote;
            accountInfo.ProfilePicName = model.ProfilePicName;

            try
            {
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            return View();
            //return RedirectToAction("Index", "Home");
        }

        public ActionResult ProfilePicture(string userName)
        {
            return View();
        }

        [HttpPost]
        public ActionResult ProfilePicture(HttpPostedFileBase file, string userName)
        {
            return View();
        }
    }
}

/*Create which is called when signing in and its properties0 defined.
 This class will be used to reference the current logged in users. This 
 to prevent from having to pass username in every fucnion.*/