using System.Collections.Generic;

namespace Lx.Utilities.Contracts.Infrastructure.Interfaces
{
    public interface IHasShareGroups
    {
        ICollection<string> ShareGroups();
        void EraseShareGroupInfoForClientSide();
    }
}