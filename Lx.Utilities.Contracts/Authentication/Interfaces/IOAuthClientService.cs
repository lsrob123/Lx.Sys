using System.Threading.Tasks;
using Lx.Utilities.Contracts.Authentication.DTOs;

namespace Lx.Utilities.Contracts.Authentication.Interfaces
{
    public interface IOAuthClientService
    {
        Task<GetTokensResponse> GetTokensAsync(GetTokensRequest request);
        Task<GetTokensResponse> RefreshTokensAsync(RefreshTokensRequest request);
        Task<GetUserInfoResponse> GetUserInfoAsync(GetUserInfoRequest request);
        Task<RevokeTokenResponse> RevokeTokenAsync(RevokeTokenRequest request);
    }
}