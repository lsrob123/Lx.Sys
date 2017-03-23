using System;

namespace Lx.Utilities.Contract.Infrastructure.Common {
    public interface IHasInstanceKey {
        Guid InstanceKey { get; }
    }
}