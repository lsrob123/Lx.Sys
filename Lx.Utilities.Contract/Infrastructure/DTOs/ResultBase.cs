using System;
using System.Collections.Generic;
using System.Linq;
using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Utilities.Contract.Infrastructure.DTOs {
    public abstract class ResultBase : IResultBase, IHasInstanceKey {
        protected ResultBase() {
            ServiceReferences = new List<string>();
            InstanceKey = Guid.NewGuid();
        }

        public Guid InstanceKey { get; set; }

        public virtual ProcessResult Result { get; set; }

        public virtual void EnsureSecurityForClientSide() {
            CleanUpForClientSide();
            EraseShareGroupInfoForClientSide();
        }

        public Guid Sid { get; set; }

        public virtual ICollection<string> ShareGroups() {
            return null;
        }

        public virtual string OriginatorGroup { get; set; }
        public virtual string RequestReference { get; set; }

        public ICollection<string> ServiceReferences { get; set; }

        public abstract void EraseShareGroupInfoForClientSide();

        protected virtual void CleanUpForClientSide() {
            Result?.SetExceptions(null);
            ServiceReferences = null;
        }

        protected virtual ICollection<string> MakeShareGroups(IEnumerable<string> shareGroups) {
            var list = shareGroups?.ToList();
            if ((list == null) || !list.Any() || string.IsNullOrWhiteSpace(OriginatorGroup))
                return list;

            list = list.Where(x => !x.Equals(OriginatorGroup, StringComparison.OrdinalIgnoreCase)).ToList();
            return list;
        }

        protected virtual ICollection<string> MakeShareGroups(params object[] shareGroups) {
            return MakeShareGroups(shareGroups?.Select(x => x.ToString()));
        }
    }
}