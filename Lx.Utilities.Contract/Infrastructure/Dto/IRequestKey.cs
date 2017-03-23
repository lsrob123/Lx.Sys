﻿using System.Collections.Generic;

namespace Lx.Utilities.Contract.Infrastructure.DTO {
    public interface IBasicRequestKey {
        string OriginatorGroup { get; set; }
        string RequestReference { get; set; }
    }

    public interface IRequestKey : IBasicRequestKey {
        ICollection<string> ServiceReferences { get; set; }
    }
}