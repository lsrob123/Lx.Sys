using System.Collections.Generic;
using Lx.Shared.All.Domains.Identity.DTOs;
using Lx.Utilities.Contracts.Infrastructure.DTOs;

namespace Lx.Shared.All.Domains.Identity.RequestsResponses
{
    public class GetUserKeyEmailsResponse : ResponseBase
    {
        public ICollection<UserKeyEmailDto> UserKeyEmails { get; set; }

        public override void EraseShareGroupInfoForClientSide()
        {
        }
    }
}