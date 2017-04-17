using System;

namespace Lx.Identity.Contracts.Interfaces
{
    public interface IHasScopeKey
    {
        Guid ScopeKey { get; } 
    }
}