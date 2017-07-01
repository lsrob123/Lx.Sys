using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Lx.Utilities.Contract.Logging;
using Lx.Utilities.Contracts.Authentication.Config;
using Lx.Utilities.Contracts.Authentication.Interfaces;
using Lx.Utilities.Contracts.Authorization;
using Lx.Utilities.Contracts.IoC;
using Lx.Utilities.Contracts.Serialization;
using Lx.Utilities.Contracts.Web;

namespace Lx.Utilities.Services.Web
{
    public class AuthorizeWithCookiesAttribute : AuthorizeAttribute, ICustomAuthorizeAttribute
    {
        protected readonly IAuthorizationService AuthorizationService;
        protected readonly IClaimProcessor ClaimProcessor;
        protected readonly ILogger Logger;
        protected readonly IOAuthHelper OAuthHelper;
        protected readonly ISerializer Serializer;
        protected readonly IOAuthClientSettings OAuthClientSettings;
        protected readonly IWebAuthenticationSettings WebAuthenticationSettings;

        public AuthorizeWithCookiesAttribute(string roles = null, string users = null, string process = null,
            string target = null, bool isDenied = false, bool getUserInfoOnly = false)
        {
            Roles = roles;
            Users = users;
            Process = process;
            Target = target;
            IsDenied = isDenied;
            GetUserInfoOnly = getUserInfoOnly;

            Logger = Logger ?? GlobalDependencyResolver.Default.ResolveRequiredDependency<ILogger>();
            OAuthHelper = OAuthHelper ?? GlobalDependencyResolver.Default.ResolveRequiredDependency<IOAuthHelper>();
            Serializer = Serializer ?? GlobalDependencyResolver.Default.ResolveRequiredDependency<ISerializer>();

            OAuthClientSettings = OAuthClientSettings ?? GlobalDependencyResolver.Default.ResolveRequiredDependency<IOAuthClientSettings>();
            ClaimProcessor = ClaimProcessor ?? GlobalDependencyResolver.Default
                                 .ResolveRequiredDependency<IClaimProcessor>();
            WebAuthenticationSettings = WebAuthenticationSettings ??
                                        GlobalDependencyResolver.Default
                                            .ResolveRequiredDependency<IWebAuthenticationSettings>();

            AuthorizationService = AuthorizationService ??
                                   GlobalDependencyResolver.Default.ResolveRequiredDependency<IAuthorizationService>();
        }

        public string Process { get; set; }
        public string Target { get; set; }
        public bool IsDenied { get; set; }

        /// <summary>
        ///     If set to true, although the authorization check will be done, the user can still access the invoked method by
        ///     having IsAuthenticated set to true
        /// </summary>
        public bool GetUserInfoOnly { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var accessToken = httpContext.Request.Cookies[WebAuthenticationSettings.AccessTokenCookie]?.Value;
            if (string.IsNullOrWhiteSpace(accessToken))
                return false;

            var accessTokenExpiryCookieValue = httpContext.Request
                .Cookies[WebAuthenticationSettings.AccessTokenExpiryToken]?.Value;
            if (string.IsNullOrWhiteSpace(accessTokenExpiryCookieValue))
                return false;

           if (!DateTimeOffset.TryParse(accessTokenExpiryCookieValue, out DateTimeOffset accessTokenExpiry))
                return false;
            if (DateTimeOffset.UtcNow.Subtract(accessTokenExpiry) > TimeSpan.Zero)
            {
               //TODO: Refresh tokens?
                
            }

            var user = OAuthHelper.GetUserAsync(accessToken).Result;
            if (user == null || !user.OriginalClaims.Any())
                return false;

            var claimsIdentity = new ClaimsIdentity(user.OriginalClaims);
            httpContext.User = new ClaimsPrincipal(claimsIdentity);

            var isAuthorized = AuthorizationService.IsAuthorized(this, user) || GetUserInfoOnly;
            return isAuthorized;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result=new RedirectResult(WebAuthenticationSettings.LoginPageUrl);
        }
    }
}