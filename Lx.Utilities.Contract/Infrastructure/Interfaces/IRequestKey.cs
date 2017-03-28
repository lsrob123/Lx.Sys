using System.Collections.Generic;

namespace Lx.Utilities.Contract.Infrastructure.Interfaces {
    public interface IBasicRequestKey {
        string OriginatorGroup { get; set; }
        string RequestReference { get; set; }
        string OriginatorConnection { get; set; }
    }

    public interface IRequestKey : IBasicRequestKey {
        ICollection<string> ServiceReferences { get; set; }
    }
}