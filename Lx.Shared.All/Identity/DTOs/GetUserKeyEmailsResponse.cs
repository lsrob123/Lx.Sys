using System.Collections.Generic;
using Lx.Utilities.Contract.Infrastructure.DTOs;

namespace Lx.Shared.All.Identity.DTOs {
    public class GetUserKeyEmailsResponse : ResponseBase {
        public ICollection<UserKeyEmailDto> UserKeyEmails { get; set; }
        public override void EraseShareGroupInfoForClientSide() {
        }
    }
}