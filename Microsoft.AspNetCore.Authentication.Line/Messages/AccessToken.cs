using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Authentication.Line.Messages
{
    public class AccessToken
    {
        public string access_token { get; set; }

        public long expires_in { get; set; }

        public string id_token { get; set; }

        public string refresh_token { get; set; }

        public string scope { get; set; }

        public string token_type { get; set; }

    }
}
