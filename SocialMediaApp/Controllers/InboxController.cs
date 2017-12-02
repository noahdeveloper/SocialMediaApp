using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialMediaApp.Controllers
{
    public class InboxController : Controller
    {
        //Home
        //Create new conversation.
        //Display conversations' links if any exist.
        public ActionResult InboxHome()
        {
            ViewBag.UserName = Request.Cookies["userNameSocialMediaApp"].Value;
            return View();
        }

        //Create new conversation
        //Choose user to start conversatoin with.
            //If conversation with user already exits then go to existing conversation.
            //If conversation does not exist then create new table and redirect to view.

        public ActionResult Create()
        {
            //Look for conversations and if some found then foeach loop show them on view.

            return View();
        }

        //Go to conversation
            //If username in conversation name then show.
            //Send message, will add row to conversation table.

        //Delete conversation
            //If username == to conversation owner in conversation table then delete, else dont allow.
    }
}