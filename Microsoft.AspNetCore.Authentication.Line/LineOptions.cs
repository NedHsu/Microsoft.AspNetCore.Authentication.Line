// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Authentication.Line
{
    /// <summary>
    /// Configuration options for <see cref="LineHandler"/>.
    /// </summary>
    public class LineOptions : OAuthOptions
    {
        /// <summary>
        /// Initializes a new <see cref="LineOptions"/>.
        /// </summary>
        public LineOptions()
        {
            CallbackPath = new PathString("/line");
            AuthorizationEndpoint = LineDefaults.AuthorizationEndpoint;
            TokenEndpoint = LineDefaults.TokenEndpoint;
            UserInformationEndpoint = LineDefaults.UserInformationEndpoint;
            Scope.Add("profile");
            Scope.Add("openid");
            Scope.Add("email");
        }

        /// <summary>
        /// Indicates whether your application can refresh access tokens when the user is not present at the browser.
        /// Valid values are <c>online</c>, which is the default value, and <c>offline</c>.
        /// <para>
        /// Set the value to offline if your application needs to refresh access tokens when the user is not present at the browser.
        /// </para>
        /// </summary>
        public string? AccessType { get; set; }
    }
}
