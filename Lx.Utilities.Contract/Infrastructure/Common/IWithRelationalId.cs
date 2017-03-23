using System;

namespace Lx.Utilities.Contract.Infrastructure.Common {
    public interface IWithRelationalId {
        long Id { get; }
        DateTimeOffset? TimeCreated { get; }
        DateTimeOffset? TimeModified { get; }
        void SetId(long id);
        void SetTimeCreated(DateTimeOffset? timeCreated);
    }
}