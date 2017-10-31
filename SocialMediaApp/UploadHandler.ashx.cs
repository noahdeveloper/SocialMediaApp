using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialMediaApp.Models;
using SocialMediaApp.Controllers;

namespace SocialMediaApp
{
    /// <summary>
    /// Summary description for UploadHandler
    /// </summary>
    public class UploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            UserAccount model = new UserAccount
            {
                UserName = "TestJS",
                Password = "TestSJ123",
                ConfimPassword = "TestSJ123"
            };

            RegisterController controller = new RegisterController();
            controller.SignUp(model);

            if (context.Request.Files.Count > 0)
            {
                HttpFileCollection files = context.Request.Files;

                for (int i = 0; i < files.Count; i++)
                {
                    System.Threading.Thread.Sleep(1000);
                    HttpPostedFile file = files[i];
                    string fileName = context.Server.MapPath("~/Uploads/" + System.IO.Path.GetFileName(file.FileName));
                    file.SaveAs(fileName);
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}