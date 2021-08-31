using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Microsoft.AspNetCore.Authentication.Line.Messages
{
    public class Profile
    {
        public string userId { get; set; }

        public string displayName { get; set; }

        public string pictureUrl { get; set; }

        public string statusMessage { get; set; }

        public string email { get; set; }

    }
}
