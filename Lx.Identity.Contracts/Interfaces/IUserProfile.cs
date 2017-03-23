using System;

namespace Lx.Identity.Contracts.Interfaces {
    //TODO: create app relation
    public interface IUserProfile {
        string Body { get; }
        Guid UserKey { get; }
    }
}