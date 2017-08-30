using System;

namespace Lx.Utilities.Contracts.Infrastructure.Interfaces
{
    public interface IHasInstanceKey
    {
        Guid InstanceKey { get; }
    }
}