using System;

namespace Lx.Identity.Contracts.Interfaces
{
    public interface IClientSecret : IHasClientKey
    {
        Guid Key { get; }
        string Value { get; }
    }
}