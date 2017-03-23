namespace Lx.Utilities.Contract.Authentication {
    public class OAuthLoginClient : IOAuthLoginClient {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public virtual bool IsValid => !string.IsNullOrWhiteSpace(ClientId) && !string.IsNullOrWhiteSpace(ClientSecret);
    }
}