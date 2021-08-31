// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Line;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods to configure Line OAuth authentication.
    /// </summary>
    public static class LineExtensions
    {
        /// <summary>
        /// Adds Line OAuth-based authentication to <see cref="AuthenticationBuilder"/> using the default scheme.
        /// The default scheme is specified by <see cref="LineDefaults.AuthenticationScheme"/>.
        /// <para>
        /// Line authentication allows application users to sign in with their Line account.
        /// </para>
        /// </summary>
        /// <param name="builder">The <see cref="AuthenticationBuilder"/>.</param>
        /// <returns>A reference to <paramref name="builder"/> after the operation has completed.</returns>
        public static AuthenticationBuilder AddLine(this AuthenticationBuilder builder)
            => builder.AddLine(LineDefaults.AuthenticationScheme, _ => { });

        /// <summary>
        /// Adds Line OAuth-based authentication to <see cref="AuthenticationBuilder"/> using the default scheme.
        /// The default scheme is specified by <see cref="LineDefaults.AuthenticationScheme"/>.
        /// <para>
        /// Line authentication allows application users to sign in with their Line account.
        /// </para>
        /// </summary>
        /// <param name="builder">The <see cref="AuthenticationBuilder"/>.</param>
        /// <param name="configureOptions">A delegate to configure <see cref="LineOptions"/>.</param>
        /// <returns>A reference to <paramref name="builder"/> after the operation has completed.</returns>
        public static AuthenticationBuilder AddLine(this AuthenticationBuilder builder, Action<LineOptions> configureOptions)
            => builder.AddLine(LineDefaults.AuthenticationScheme, configureOptions);

        /// <summary>
        /// Adds Line OAuth-based authentication to <see cref="AuthenticationBuilder"/> using the default scheme.
        /// The default scheme is specified by <see cref="LineDefaults.AuthenticationScheme"/>.
        /// <para>
        /// Line authentication allows application users to sign in with their Line account.
        /// </para>
        /// </summary>
        /// <param name="builder">The <see cref="AuthenticationBuilder"/>.</param>
        /// <param name="authenticationScheme">The authentication scheme.</param>
        /// <param name="configureOptions">A delegate to configure <see cref="LineOptions"/>.</param>
        /// <returns>A reference to <paramref name="builder"/> after the operation has completed.</returns>
        public static AuthenticationBuilder AddLine(this AuthenticationBuilder builder, string authenticationScheme, Action<LineOptions> configureOptions)
            => builder.AddLine(authenticationScheme, LineDefaults.DisplayName, configureOptions);

        /// <summary>
        /// Adds Line OAuth-based authentication to <see cref="AuthenticationBuilder"/> using the default scheme.
        /// The default scheme is specified by <see cref="LineDefaults.AuthenticationScheme"/>.
        /// <para>
        /// Line authentication allows application users to sign in with their Line account.
        /// </para>
        /// </summary>
        /// <param name="builder">The <see cref="AuthenticationBuilder"/>.</param>
        /// <param name="authenticationScheme">The authentication scheme.</param>
        /// <param name="displayName">A display name for the authentication handler.</param>
        /// <param name="configureOptions">A delegate to configure <see cref="LineOptions"/>.</param>
        /// <returns>A reference to <paramref name="builder"/> after the operation has completed.</returns>
        public static AuthenticationBuilder AddLine(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<LineOptions> configureOptions)
            => builder.AddOAuth<LineOptions, LineHandler>(authenticationScheme, displayName, configureOptions);
    }
}
