using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Utilities.Contract.Authentication.DTOs
{
    public class OAuthLoginClient : IOAuthLoginClient, IDto
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public virtual bool IsValid => !string.IsNullOrWhiteSpace(ClientId) && !string.IsNullOrWhiteSpace(ClientSecret);
    }
}