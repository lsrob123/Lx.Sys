using System;
using System.Collections.Generic;

namespace Lx.Shared.All.Identity.DTOs {
    public class GetUserKeyEmailsRequest : RequestBase {
        public List<Guid> UserKeys { get; set; }
    }
}