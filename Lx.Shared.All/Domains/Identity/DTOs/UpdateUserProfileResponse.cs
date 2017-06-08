using Lx.Utilities.Contract.Infrastructure.DTOs;

namespace Lx.Shared.All.Domains.Identity.DTOs {
    public class UpdateUserProfileResponse : ResponseBase {
        public override void EraseShareGroupInfoForClientSide() {
        }
    }
}