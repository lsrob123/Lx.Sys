using System;

namespace Lx.Identity.Contracts.Interfaces {
    public interface IClientScope : IHasClientKey
    {
        Guid Key { get; }
        string Scope { get; }
    }
}