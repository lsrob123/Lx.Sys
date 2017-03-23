using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Lx.Utilities.Contract.Authentication;
using Lx.Utilities.Contract.Authentication.Constants;
using Lx.Utilities.Contract.Authorization;
using Lx.Utilities.Contract.IoC;
using Lx.Utilities.Contract.Logging;
using Lx.Utilities.Contract.Serialization;
using Lx.Utilities.Services.Infrastructure;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Owin;
using IRequest = Lx.Utilities.Contract.Infrastructure.Dto.IRequest;

namespace Lx.Utilities.Services.SignalR {
    /// <summary>
    ///     TODO: Add unit tests
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class OAuthAuthorizeAttribute : AuthorizeAttribute, IAccessCriteria {
        protected const string UserEnvironmentField = "server.User";
        protected readonly IAuthorizationService AuthorizationService;
        protected readonly ILogger Logger;
        protected readonly IOAuthHelper OAuthHelper;
        protected readonly ISerializer Serializer;

        public OAuthAuthorizeAttribute(string roles = null, string users = null, string process = null,
            string target = null, bool isDenied = false) {
            Roles = roles;
            Users = users;
            Process = process;
            Target = target;
            IsDenied = isDenied;

            Logger = Logger ?? GlobalDependencyResolver.Default.ResolveRequiredDependency<ILogger>();
            OAuthHelper = OAuthHelper ?? GlobalDependencyResolver.Default.ResolveRequiredDependency<IOAuthHelper>();
            Serializer = Serializer ?? GlobalDependencyResolver.Default.Resolve<ISerializer>();

            AuthorizationService = AuthorizationService ??
                                   GlobalDependencyResolver.Default.ResolveRequiredDependency<IAuthorizationService>();
        }

        /// <summary>
        ///     If set to true, although the authorization check will be done, the user can still access the invoked method by
        ///     having IsAuthenticated set to true
        /// </summary>
        public bool GetUserInfoOnly { get; set; }

        public string Process { get; set; }

        public string Target { get; set; }

        public bool IsDenied { get; set; }

        public override bool AuthorizeHubMethodInvocation(IHubIncomingInvokerContext hubIncomingInvokerContext,
            bool appliesToMethod) {
            string accessToken = null;
            if ((hubIncomingInvokerContext.Args != null) && hubIncomingInvokerContext.Args.Any()) {
                var request = hubIncomingInvokerContext.Args[0] as IRequest;
                if (!string.IsNullOrWhiteSpace(request?.AccessToken)) {
                    accessToken = request.AccessToken;
                } else {
                    var header = hubIncomingInvokerContext.Hub.Context.Request.Headers
                        .FirstOrDefault(x => (x.Key == "Authorization") && x.Value.Contains("Bearer "))
                        .Value;
                    if (header != null) {
                        accessToken = header.Replace("Bearer ", string.Empty);
                    } else {
                        var cookie = hubIncomingInvokerContext.Hub.Context.Request.Cookies
                            .FirstOrDefault(x => x.Key.ToLower().Contains("access") && x.Key.ToLower().Contains("token"))
                            .Value;
                        if (!string.IsNullOrWhiteSpace(cookie.Value)) {
                            accessToken = cookie.Value;
                        } else {
                            var queryStringParameter = hubIncomingInvokerContext.Hub.Context.Request.QueryString
                                .FirstOrDefault(
                                    x => x.Key.ToLower().Contains("access") && x.Key.ToLower().Contains("token"))
                                .Value;
                            if (!string.IsNullOrWhiteSpace(queryStringParameter))
                                accessToken = queryStringParameter;
                        }
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(accessToken)) {
                new NullReferenceException(nameof(accessToken)).WriteToLog(Logger);
                return GetUserInfoOnly;
            }

            try {
                var isAuthorized = SetUser(hubIncomingInvokerContext, accessToken);

                return GetUserInfoOnly || isAuthorized;
            } catch (Exception ex) {
                ex.WriteToLog(Logger);
                return false;
            }
        }

        protected virtual bool SetUser(IHubIncomingInvokerContext hubIncomingInvokerContext, string accessToken) {
            var user = OAuthHelper.GetUserAsync(accessToken).Result;
            if (user == null)
                return false;

            var isAuthorized = AuthorizationService.IsAuthorized(this, user);

            var claims = new List<Claim>(user.OriginalClaims) {
                new Claim(ClaimType.IsAuthorized, isAuthorized.ToString())
            };
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims));

            var environment = hubIncomingInvokerContext.Hub.Context.Request.Environment;
            environment[UserEnvironmentField] = claimsPrincipal;

            var context = new HubCallerContext(new ServerRequest(environment),
                hubIncomingInvokerContext.Hub.Context.ConnectionId);

            hubIncomingInvokerContext.Hub.Context = context;
            return isAuthorized;
        }
    }
}