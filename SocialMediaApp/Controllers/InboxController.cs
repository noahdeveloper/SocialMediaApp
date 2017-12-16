using SocialMediaApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public ActionResult InboxHome(string user, bool? conversationExist)
        {
            string userName = String.Empty;

            if (user == null)
            {
                userName = Request.Cookies["userNameSocialMediaApp"].Value;
            }
            else
            {
                userName = user;
            }

            ViewBag.UserName = userName;

            //List conversation
            var conversationsOwned = _context.Conversation.Where(x => x.Owner == userName).Select(x => x.CoOwner).ToList();
            var conversationsCoOwned = _context.Conversation.Where(x => x.CoOwner == userName).Select(x => x.Owner).ToList();

            foreach (var convo in conversationsCoOwned)
            {
                conversationsOwned.Add(convo);
            }

            InboxViewModel viewModel = new InboxViewModel(conversationsOwned);

            if (conversationsOwned == null)
            {
                ViewBag.NoConversation = true;
                return View(viewModel);
            }

            if (conversationExist == true)
            {
                ViewBag.conversationExist = true;
                return View(viewModel);
            }

            return View(viewModel);
        }

        public ActionResult WithWho()
        {
            ConversationViewModel model = new ConversationViewModel();

            string userName = Request.Cookies["userNameSocialMediaApp"].Value;

            model.AllUsers = (from x in _context.UserInformation where x.ID != userName select x.ID).ToList();

            ViewBag.UserName = userName;
            return View(model);
        }

        public ActionResult Create(string conversationCoOwner)
        {
           string userName  = Request.Cookies["userNameSocialMediaApp"].Value;

            ViewBag.UserName = userName;

            //Look for conversations and if some found then foeach loop show them on view.

            ConversationViewModel model = new ConversationViewModel();
            model.ConversationCoOwner = conversationCoOwner;

            var conversationFound = _context.Conversation.Where(x => x.Owner == userName && x.CoOwner == model.ConversationCoOwner).Select(x => x).FirstOrDefault();

            if (conversationFound != null)
            {
                return RedirectToAction("InboxHome", new { conversationExist = true });
            }

            model.AllUsers = (from x in _context.UserInformation select x.ID).ToList();

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

            //Find conversation
            var conversationFound = _context.Conversation.Where(x => x.ConversationName == model2.ConversationName).Select(x => x.ConversationName).FirstOrDefault();

            //If not found then create and send initial message.
            if (conversationFound == null)
            {
                //Add to parent table
                _context.Conversation.Add(model2);

                //Add to child/content table
                ConversationContent model3 = new ConversationContent();
                model3.ConversationName = model2.ConversationName;
                model3.SentFrom = model2.Owner;
                model3.Message = model.InitialMessage;
                model3.SentTo = model2.CoOwner;
                model3.Date = DateTime.Now;
                model3.FromOwner = true;

                _context.ConversationContent.Add(model3);

                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                }
            }
            else //redirect to existing conversation
            {
                return RedirectToAction("InboxHome", new { user = model2.Owner, conversationExist = true });
            }

            return RedirectToAction("InboxHome", new { user = model2.Owner});
        }

        public bool CheckIfTableExist(string tableName)
        {
            var tableInDB = Database.Exists(tableName);

            if (tableInDB)
                return true;

            return false;
        }

        //public ActionResult GoToConversation(string conversationCoOwner)
        //{
        //    string conversationName = String.Empty;

        //    string owner = Request.Cookies["userNameSocialMediaApp"].Value;
        //    string currentUser = Request.Cookies["userNameSocialMediaApp"].Value;

        //    conversationName += owner;
        //    conversationName += "-" + conversationCoOwner;

        //    var conversation = _context.ConversationContent.Where(x => x.ConversationName == conversationName).OrderBy(x => x.Date).ToList();

        //    ConversationContentViewModel message = new ConversationContentViewModel();

        //    if (conversation.Count() == 0)
        //    {
        //        //try a usernames backwards.
        //        string conversatinName2 = String.Empty;
        //        conversatinName2 += conversationCoOwner;
        //        conversatinName2 += "-" + owner;
        //        var conversation2 = _context.ConversationContent.Where(x => x.ConversationName == conversatinName2).OrderBy(x => x.Date).ToList();

        //        message.Owner = owner;
        //        message.CoOnwer = conversationCoOwner;
        //        message.ConversationName = conversatinName2;
        //        message.Messages = conversation2;

        //        ViewBag.UserName = owner;
        //        return View(message);
        //    }

        //    message.Owner = owner;
        //    message.CoOnwer = conversationCoOwner;
        //    message.ConversationName = conversationName;
        //    message.Messages = conversation;

        //    ViewBag.UserName = owner;
        //    return View(message);
        //}

        public ActionResult GoToConversation(string conversationCoOwner)
        {
            ConversationContentViewModel message = new ConversationContentViewModel();
            string currentUser = Request.Cookies["userNameSocialMediaApp"].Value;

            string conversationName = String.Empty;
            conversationName += currentUser;
            conversationName += "-" + conversationCoOwner;
            var conversationPart1 = _context.ConversationContent.Where(x => x.ConversationName == conversationName).OrderBy(x => x.Date).ToList();

            string conversationName2 = String.Empty;
            conversationName2 += conversationCoOwner;
            conversationName2 += "-" + currentUser;
            var conversatioPart2 = _context.ConversationContent.Where(x => x.ConversationName == conversationName2).OrderBy(x => x.Date).ToList();

            var conversation = conversationPart1.Union(conversatioPart2).OrderBy(x => x.Date).ToList();

            message.ConversationName = conversationName;
            message.SentFrom = currentUser;
            message.SentTo = conversationCoOwner;
            message.Messages = conversation;
            message.CoOwner = conversationCoOwner;

            ViewBag.UserName = currentUser;
            return View(message);
        }

        [HttpPost]
        public ActionResult GoToConversation(ConversationContentViewModel message)
        {
            string currentUser = Request.Cookies["userNameSocialMediaApp"].Value;

            ConversationContent newMessage = new ConversationContent();
            newMessage.ConversationName = message.ConversationName;
            newMessage.SentFrom = currentUser;
            newMessage.Message = message.NewMessage;
            newMessage.SentTo = message.CoOwner;
            newMessage.Date = DateTime.Now;

            _context.ConversationContent.Add(newMessage);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

            string conversationName = String.Empty;
            conversationName += currentUser;
            conversationName += "-" + message.CoOwner;
            var conversationPart1 = _context.ConversationContent.Where(x => x.ConversationName == conversationName).OrderBy(x => x.Date).ToList();

            string conversationName2 = String.Empty;
            conversationName2 += message.CoOwner;
            conversationName2 += "-" + currentUser;
            var conversatioPart2 = _context.ConversationContent.Where(x => x.ConversationName == conversationName2).OrderBy(x => x.Date).ToList();

            var conversation = conversationPart1.Union(conversatioPart2).OrderBy(x => x.Date).ToList();

            message.Messages = conversation;
            message.NewMessage = "";
            ViewBag.UserName = currentUser;
            return View(message);
        }
    }
}