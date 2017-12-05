using SocialMediaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialMediaApp.Controllers
{
    public class InboxController : Controller
    {
        ApplicationDbContext _context;

        public InboxController()
        {
            _context = new ApplicationDbContext();
        }

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

        public ActionResult WithWho()
        {
            ConversationViewModel model = new ConversationViewModel();

            model.AllUsers = (from x in _context.UserInformation select x.ID).ToList();

            ViewBag.UserName = Request.Cookies["userNameSocialMediaApp"].Value;
            return View(model);
        }

        public ActionResult Create(string conversationCoOwner)
        {
            //Look for conversations and if some found then foeach loop show them on view.

            ConversationViewModel model = new ConversationViewModel();
            model.ConversationCoOwner = conversationCoOwner;

            model.AllUsers = (from x in _context.UserInformation select x.ID).ToList();

            ViewBag.UserName = Request.Cookies["userNameSocialMediaApp"].Value;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ConversationViewModel model)
        {
            Conversation model2 = new Conversation();
            model2.Owner = Request.Cookies["userNameSocialMediaApp"].Value;
            model2.CoOwner = model.ConversationCoOwner;
            model2.ConversationName = model2.Owner + "-" + model2.CoOwner;
            model2.CreatedDate = DateTime.Now;

            var conversationFound = _context.Conversation.Where(x => x.ConversationName == model2.ConversationName).Select(x => x.ConversationName).FirstOrDefault();

            if (conversationFound == null)
            {
                _context.Conversation.Add(model2);

                try
                {
                    _context.SaveChanges();
                }
                catch(Exception ex)
                {
                    string error = ex.Message;
                }
            }

            return View();
        }

        //Go to conversation
            //If username in conversation name then show.
            //Send message, will add row to conversation table.

        //Delete conversation
            //If username == to conversation owner in conversation table then delete, else dont allow.
    }
}