using System.Collections.Generic;

namespace Lx.Utilities.Contract.Infrastructure.Dto {
    public interface IHasShareGroups {
        ICollection<string> ShareGroups();
        void EraseShareGroupInfoForClientSide();
    }
}