using System;
using Lx.Utilities.Contract.Infrastructure.Common;

namespace Lx.Utilities.Contract.Infrastructure.Domain {
    public interface IEntity : IWithRelationalId {
        Guid Key { get; }
        void SetKey(Guid key);
        Guid EnsureValidKey();
        void AssignDefaultValuesToComplexPropertiesIfNull();
    }
}