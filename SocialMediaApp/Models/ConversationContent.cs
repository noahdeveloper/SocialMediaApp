using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialMediaApp.Models
{
    public class ConversationContent
    {
        public int ID { get; set; }
        public string ConversationName { get; set; }
        public string SentFrom { get; set; }
        public string Message { get; set; }
        public string SentTo { get; set; }
        public DateTime Date { get; set; }
        public bool FromOwner { get; set; }
    }
}