using Lx.Utilities.Contract.Infrastructure.DTO;

namespace Lx.Utilities.Contract.Authentication.DTOs {
    public class RevokeTokenResponse : ResponseBase {
        public override void EraseShareGroupInfoForClientSide() {}
    }
}