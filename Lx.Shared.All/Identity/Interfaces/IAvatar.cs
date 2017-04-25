using System;
using Lx.Shared.All.Common.Interfaces;

namespace Lx.Shared.All.Identity.Interfaces
{
    public interface IAvatar : IWithUserKey, IDeactivatable, IUploadFileInfo
    {
        string UriRelative { get; }
        string UriAbsoluteDefault { get; }
        int? Width { get; }
        int? Height { get; }
    }
}