using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Microsoft.AspNetCore.Authentication.Line.Messages
{
    public class Verify
    {
        public string email { get; set; }

    }
}
