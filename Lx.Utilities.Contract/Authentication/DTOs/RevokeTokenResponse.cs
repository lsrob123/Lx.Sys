using Lx.Utilities.Contract.Infrastructure.Dto;

namespace Lx.Utilities.Contract.Authentication.DTOs {
    public class RevokeTokenResponse : ResponseBase {
        public override void EraseShareGroupInfoForClientSide() {}
    }
}