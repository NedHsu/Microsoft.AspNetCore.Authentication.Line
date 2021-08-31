using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Line.Messages;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Microsoft.AspNetCore.Authentication.Line
{
    /// <summary>
    /// Authentication handler for Line's OAuth based authentication.
    /// </summary>
    public class LineHandler : OAuthHandler<LineOptions>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="LineHandler"/>.
        /// </summary>
        /// <inheritdoc />
        public LineHandler(IOptionsMonitor<LineOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        { }

        protected override async Task<HandleRequestResult> HandleRemoteAuthenticateAsync()
        {
            var query = Request.Query;
            var state = query["state"];
            AuthenticationProperties properties = Options.StateDataFormat.Unprotect(state);

            if (properties == null)
            {
                return HandleRequestResult.Fail("Invalid state cookie.");
            }

            var code = query["code"];
            if (string.IsNullOrEmpty(code))
            {
                return HandleRequestResult.Fail("Missing code");
            }

            var response = await Backchannel.PostAsync(LineDefaults.RequestTokenEndpoint, new FormUrlEncodedContent(new Dictionary<string, string>{
                { "grant_type", "authorization_code" },
                { "code", code },
                { "client_id", Options.ClientId },
                { "client_secret", Options.ClientSecret },
                { "redirect_uri", properties.GetString("redirect_uri") },
            }));

            if (!response.IsSuccessStatusCode)
            {
                return HandleRequestResult.Fail(await response.Content.ReadAsStringAsync());
            }

            var accessToken = JsonConvert.DeserializeObject<AccessToken>(await response.Content.ReadAsStringAsync());

            var tiket = await CreateTicketAsync(properties, accessToken);
            return HandleRequestResult.Success(tiket);
        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            return base.HandleChallengeAsync(properties);
        }

        /// <inheritdoc />
        protected virtual async Task<AuthenticationTicket> CreateTicketAsync(
            AuthenticationProperties properties,
            AccessToken tokens)
        {
            var identity = new ClaimsIdentity(ClaimsIssuer);

            // Get the Line user
            await GetProfile(tokens, identity);

            // Vertify (Get Email)
            await Vertify(tokens, identity);

            return new AuthenticationTicket(new ClaimsPrincipal(identity), properties, Scheme.Name);
        }

        private async Task Vertify(AccessToken tokens, ClaimsIdentity identity)
        {
            var response = await Backchannel.PostAsync(Options.UserInformationEndpoint, new FormUrlEncodedContent(new Dictionary<string, string> {
                 { "id_token", tokens.id_token },
                 { "client_id", Options.ClientId }
                }));
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"An error occurred when retrieving Line user information ({response.StatusCode}). Please check if the authentication information is correct.");
            }
            var payload = JsonConvert.DeserializeObject<Verify>(await response.Content.ReadAsStringAsync());
            identity.AddClaim(new Claim(ClaimTypes.Email, payload.email, ClaimValueTypes.String, ClaimsIssuer));
        }

        private async Task GetProfile(AccessToken tokens, ClaimsIdentity identity)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, LineDefaults.UserProfileEndpoint);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokens.access_token);

            var response = await Backchannel.SendAsync(request, Context.RequestAborted);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"An error occurred when retrieving Line user profile ({response.StatusCode}). Please check if the authentication information is correct.");
            }

            var payload = JsonConvert.DeserializeObject<Profile>(await response.Content.ReadAsStringAsync());
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, payload.userId, ClaimValueTypes.String, ClaimsIssuer));
            identity.AddClaim(new Claim(ClaimTypes.Name, payload.displayName, ClaimValueTypes.String, ClaimsIssuer));
        }

        /// <inheritdoc />
        protected override string BuildChallengeUrl(AuthenticationProperties properties, string redirectUri)
        {
            var queryStrings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            queryStrings.Add("response_type", "code");
            properties.Items.Add("grant_type", "authorization_code");

            AddQueryString(queryStrings, properties, "client_id", Options.ClientId);
            AddQueryString(queryStrings, properties, "redirect_uri", redirectUri);
            AddQueryString(queryStrings, properties, "scope", "profile openid email");

            var state = Options.StateDataFormat.Protect(properties);
            queryStrings.Add("state", state);

            var authorizationEndpoint = QueryHelpers.AddQueryString(Options.AuthorizationEndpoint, queryStrings!);
            return authorizationEndpoint;
        }

        private static void AddQueryString(
            IDictionary<string, string> queryStrings,
            AuthenticationProperties properties,
            string name,
            string value = null)
        {
            queryStrings.Add(name, value);
            properties.Items.Add(name, value);
        }

    }
}
