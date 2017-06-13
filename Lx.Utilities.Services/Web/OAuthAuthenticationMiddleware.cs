using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Lx.Utilities.Contracts.Authentication.Config;
using Lx.Utilities.Contracts.Authentication.Interfaces;
using Lx.Utilities.Contracts.Logging;
using Microsoft.Owin;

namespace Lx.Utilities.Services.Web
{
    public class OAuthAuthenticationMiddleware : OwinMiddleware
    {
        protected const string AuthorizationHeaderName = "Authorization";
        protected const string BearerPrefix = "Bearer ";
        protected readonly IClaimProcessor ClaimProcessor;
        protected readonly ILogger Logger;
        protected readonly IOAuthHelper OAuthHelper;
        protected readonly IOAuthClientSettings Settings;

        public OAuthAuthenticationMiddleware(OwinMiddleware next, ILogger logger, IOAuthClientSettings settings,
            IClaimProcessor claimProcessor, IOAuthHelper oauthHelper) : base(next)
        {
            Logger = logger;
            Settings = settings;
            ClaimProcessor = claimProcessor;
            OAuthHelper = oauthHelper;
        }

        public override async Task Invoke(IOwinContext context)
        {
            string[] authorizationHeaders;
            if (!context.Request.Headers.TryGetValue(AuthorizationHeaderName, out authorizationHeaders))
            {
                await Next.Invoke(context);
                return;
            }

            var headerWithBearerToken = authorizationHeaders.FirstOrDefault(x =>
                x.StartsWith(BearerPrefix, StringComparison.Ordinal));
            if (headerWithBearerToken == null)
            {
                await Next.Invoke(context);
                return;
            }

            var accessToken = headerWithBearerToken.Replace(BearerPrefix, string.Empty);
            var user = await OAuthHelper.GetUserAsync(accessToken);
            if (user == null || !user.OriginalClaims.Any())
            {
                await Next.Invoke(context);
                return;
            }

            var claimsIdentity = new ClaimsIdentity(user.OriginalClaims, headerWithBearerToken);
            context.Request.User = new ClaimsPrincipal(claimsIdentity);

            await Next.Invoke(context);
        }
    }
}