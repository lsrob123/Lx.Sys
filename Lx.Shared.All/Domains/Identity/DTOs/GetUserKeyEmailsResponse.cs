using System.Collections.Generic;
using Lx.Utilities.Contracts.Infrastructure.DTOs;

namespace Lx.Shared.All.Domains.Identity.DTOs
{
    public class GetUserKeyEmailsResponse : ResponseBase
    {
        public ICollection<UserKeyEmailDto> UserKeyEmails { get; set; }

        public override void EraseShareGroupInfoForClientSide()
        {
        }
    }
}