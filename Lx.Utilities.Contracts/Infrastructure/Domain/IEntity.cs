using System;
using Lx.Utilities.Contracts.Infrastructure.Interfaces;

namespace Lx.Utilities.Contracts.Infrastructure.Domain
{
    public interface IEntity : IWithRelationalId
    {
        Guid Key { get; }
        void SetKey(Guid key);
        Guid EnsureValidKey();
        void AssignDefaultValuesToComplexPropertiesIfNull();
    }
}