using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialMediaApp.Controllers
{
    public partial class CurrentUser
    {
        public string User { get; set; }
        public string user = String.Empty;
    }

    public partial class CurrentUser
    {
        void SetUser(string User)
        {
            this.user = User;
        }
    }

}