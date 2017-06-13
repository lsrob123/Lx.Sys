using Lx.Utilities.Contracts.Infrastructure.DTOs;

namespace Lx.Shared.All.Domains.Identity.DTOs
{
    public class UpdateUserResponse : ResponseBase
    {
        public override void EraseShareGroupInfoForClientSide()
        {
        }
    }
}