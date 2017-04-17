using System;

namespace Lx.Identity.Contracts.Interfaces
{
    public interface IHasClientKey
    {
        Guid ClientKey { get; } 
    }
}