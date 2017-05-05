using System;
using Lx.Shared.All.Common.Interfaces;

namespace Lx.Shared.All.Identity.Interfaces
{
    //TODO: Create IAvatarSafe and IAvatarFull is needed
    public interface IAvatar : IWithUserKey, IDeactivatable, IUploadFileInfo
    {
        string UriRelative { get; }
        string UriDefault { get; }
        int? Width { get; }
        int? Height { get; }
    }
}