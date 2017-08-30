using Lx.Utilities.Contracts.Infrastructure.DTOs;

namespace Lx.Shared.All.Domains.Identity.RequestsResponses
{
    public class UpdateUserProfileResponse : ResponseBase
    {
        public override void EraseShareGroupInfoForClientSide()
        {
        }
    }
}