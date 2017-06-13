using System;
using System.Collections.Generic;
using Lx.Utilities.Contracts.Infrastructure.DTOs;

namespace Lx.Shared.All.Domains.Identity.DTOs
{
    public class GetUserKeyEmailsRequest : RequestBase
    {
        public List<Guid> UserKeys { get; set; }
    }
}