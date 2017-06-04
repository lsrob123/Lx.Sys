using System;

namespace Lx.Shared.All.Domains.Identity.Interfaces {
    public interface IUserProfile : IHasUserProfileOriginator {
        Guid Key { get; }
        string Body { get; }
        Guid UserKey { get; }
    }
}