using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using Lx.Utilities.Contract.Authentication;
using Lx.Utilities.Contract.Authentication.DTOs;
using Lx.Utilities.Contract.Authentication.Interfaces;
using Lx.Utilities.Contract.Authorization;
using Lx.Utilities.Contract.IoC;
using Lx.Utilities.Contract.Logging;
using Lx.Utilities.Contract.Serialization;
using Lx.Utilities.Services.Authentication;

namespace Lx.Utilities.Services.Web
{
    public class AuthorizeWithUserInRequestAttribute : AuthorizeAttribute, ICustomAuthorizeAttribute
    {
        protected readonly IAuthorizationService AuthorizationService;
        protected readonly ILogger Logger;
        protected readonly IOAuthHelper OAuthHelper;
        protected readonly ISerializer Serializer;

        public AuthorizeWithUserInRequestAttribute(string roles = null, string users = null, string process = null,
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

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var user = new IdentityDto().WithPrincipal(actionContext.RequestContext.Principal);
            var isAuthorized = AuthorizationService.IsAuthorized(this, user) || GetUserInfoOnly;

            if (!isAuthorized)
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }
    }
}