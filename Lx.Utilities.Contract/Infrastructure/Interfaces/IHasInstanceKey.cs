using System;

namespace Lx.Utilities.Contract.Infrastructure.Interfaces
{
    public interface IHasInstanceKey
    {
        Guid InstanceKey { get; }
    }
}