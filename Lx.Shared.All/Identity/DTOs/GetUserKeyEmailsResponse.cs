using System.Collections.Generic;

namespace Lx.Shared.All.Identity.DTOs {
    public class GetUserKeyEmailsResponse : ResponseBase {
        public ICollection<UserKeyEmailDto> UserKeyEmails { get; set; }
        public override void EraseShareGroupInfoForClientSide() {
        }
    }
}