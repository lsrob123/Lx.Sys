using System;

namespace Lx.Shared.All.Identity.Interfaces {
    public interface IUserProfile : IHasUserProfileOriginator {
        string Body { get; }
        Guid UserKey { get; }
    }
}