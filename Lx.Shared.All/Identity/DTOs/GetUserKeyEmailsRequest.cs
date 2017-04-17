using System;
using System.Collections.Generic;
using Lx.Utilities.Contract.Infrastructure.DTOs;

namespace Lx.Shared.All.Identity.DTOs {
    public class GetUserKeyEmailsRequest : RequestBase {
        public List<Guid> UserKeys { get; set; }
    }
}