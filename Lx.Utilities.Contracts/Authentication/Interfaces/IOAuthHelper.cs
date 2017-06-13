using System.Threading.Tasks;
using Lx.Utilities.Contracts.Authentication.DTOs;

namespace Lx.Utilities.Contracts.Authentication.Interfaces
{
    public interface IOAuthHelper
    {
        Task<IdentityDto> GetUserAsync(string accessToken);
    }
}