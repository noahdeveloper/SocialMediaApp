using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SocialMediaApp.Models
{
    public class Conversation
    {
        public int ID { get; set; }
        public string ConversationName { get; set; }
        public string Owner { get; set; }
        public string CoOwner { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class ConversationViewModel
    {
        public string ConversationOwner { get; set; }

        [DisplayName("Send To:")]
        public string ConversationCoOwner { get; set; }

        [DisplayName("Message: ")]
        public string InitialMessage { get; set; }
        public List<string> AllUsers { get; set; }
    }
}