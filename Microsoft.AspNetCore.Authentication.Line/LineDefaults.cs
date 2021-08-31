// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;

namespace Microsoft.AspNetCore.Authentication.Line
{
    /// <summary>
    /// Default values for Line authentication
    /// </summary>
    public static class LineDefaults
    {
        /// <summary>
        /// The default scheme for Line authentication. Defaults to <c>Line</c>.
        /// </summary>
        public const string AuthenticationScheme = "Line";

        /// <summary>
        /// The default display name for Line authentication. Defaults to <c>Line</c>.
        /// </summary>
        public static readonly string DisplayName = "Line";

        /// <summary>
        /// The default endpoint used to perform Line authentication.
        /// </summary>
        /// <remarks>
        /// For more details about this endpoint, see https://developers.google.com/identity/protocols/oauth2/web-server#httprest
        /// </remarks>
        public static readonly string AuthorizationEndpoint = "https://access.line.me/oauth2/v2.1/authorize";

        /// <summary>
        /// The OAuth endpoint used to exchange access tokens.
        /// </summary>
        public static readonly string TokenEndpoint = "https://api.line.me/v2/oauth/accessToken";

        /// <summary>
        /// The OAuth endpoint used to reques access tokens.
        /// </summary>
        public static readonly string RequestTokenEndpoint = "https://api.line.me/oauth2/v2.1/token";

        /// <summary>
        /// The Line endpoint that is used to gather additional user information.
        /// </summary>
        /// <remarks>
        /// For more details about this endpoint, see https://developers.google.com/apis-explorer/#search/oauth2/oauth2/v2/.
        /// </remarks>
        public static readonly string UserInformationEndpoint = "https://api.line.me/oauth2/v2.1/verify";

        /// <summary>
        /// The Line endpoint that is used to get profile
        /// </summary>
        public static readonly string UserProfileEndpoint = "https://api.line.me/v2/profile";
    }
}
