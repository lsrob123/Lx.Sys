using System.Collections.Generic;

namespace Lx.Utilities.Contract.Infrastructure.Interfaces
{
    public interface IRequestKey : IBasicRequestKey
    {
        ICollection<string> ServiceReferences { get; set; }
    }
}