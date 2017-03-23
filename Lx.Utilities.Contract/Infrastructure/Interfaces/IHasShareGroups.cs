using System.Collections.Generic;

namespace Lx.Utilities.Contract.Infrastructure.Interfaces {
    public interface IHasShareGroups {
        ICollection<string> ShareGroups();
        void EraseShareGroupInfoForClientSide();
    }
}