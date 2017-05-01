using System.Security.Principal;
using Lx.Utilities.Contract.Authentication.DTOs;
using Lx.Utilities.Services.Authentication;

namespace Lx.Utilities.Services.Web {
    public static class PrincipalExtensions {
        public static IdentityDto Current<TPrincipal>(this TPrincipal principal) where TPrincipal : IPrincipal {
            return new IdentityDto().WithPrincipal(principal);
        }
    }
}