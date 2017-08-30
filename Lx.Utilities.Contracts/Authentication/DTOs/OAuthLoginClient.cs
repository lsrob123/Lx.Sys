using Lx.Utilities.Contracts.Authentication.Interfaces;
using Lx.Utilities.Contracts.Infrastructure.Interfaces;

namespace Lx.Utilities.Contracts.Authentication.DTOs
{
    public class OAuthLoginClient : IOAuthLoginClient, IDto
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public virtual bool IsValid => !string.IsNullOrWhiteSpace(ClientId) && !string.IsNullOrWhiteSpace(ClientSecret);
    }
}