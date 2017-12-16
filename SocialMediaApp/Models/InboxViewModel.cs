using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialMediaApp.Models
{
    public class InboxViewModel
    {
        public List<string> Conversations { get; set; }

        public InboxViewModel(List<string> conversation)
        {
            Conversations = new List<string>();

            foreach(var convo in conversation)
            {
                Conversations.Add(convo);
            }
        }
    }

    public class ConversationsViewModel
    {
        public string Name { get; set; }
        public string Owner { get; set; }
        public string CoOnwer { get; set; }
    }

    public class ConversationContentViewModel
    {
        public List<ConversationContent> Messages { get; set; }

        [Required]
        public string NewMessage { get; set; }
        public string SentFrom { get; set; }
        public string SentTo { get; set; }
        public string Owner { get; set; }
        public string CoOwner { get; set; }
        public string ConversationName { get; set; }
    }
}