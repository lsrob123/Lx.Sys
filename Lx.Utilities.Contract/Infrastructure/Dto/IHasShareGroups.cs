using System.Collections.Generic;

namespace Lx.Utilities.Contract.Infrastructure.DTO {
    public interface IHasShareGroups {
        ICollection<string> ShareGroups();
        void EraseShareGroupInfoForClientSide();
    }
}