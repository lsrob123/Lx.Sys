using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Lx.Utilities.Contract.Authentication;
using Lx.Utilities.Contract.Authentication.Config;
using Lx.Utilities.Contract.Logging;
using Microsoft.Owin;

namespace Lx.Utilities.Services.Authentication
{
    public class OAuthAuthenticationMiddleware : OwinMiddleware
    {
        protected const string BearerHeader = "Bearer ";
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
            if (context.Request.Headers.TryGetValue("Authorization", out authorizationHeaders))
            {
                var headerWithBearerToken =
                    authorizationHeaders.FirstOrDefault(x => x.StartsWith(BearerHeader, StringComparison.Ordinal));
                if (headerWithBearerToken != null)
                {
                    var accessToken = headerWithBearerToken.Replace(BearerHeader, string.Empty);
                    var user = await OAuthHelper.GetUserAsync(accessToken);

                    context.Request.User = user.OriginalClaims.Any()
                        ? new ClaimsPrincipal(new ClaimsIdentity(user.OriginalClaims))
                        : null;
                }
            }

            await Next.Invoke(context);
        }
    }
}