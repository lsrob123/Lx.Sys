using System;
using Lx.Shared.All.Identity.Interfaces;
using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Shared.All.Identity.DTOs
{
    public class AvatarDto : IDto, IAvatar
    {
        public Guid UserKey { get; set; }
        public bool Deactivated { get; set; }
        public string Description { get; set; }
        public string FullFilePath { get; set; }
        public string UriRelative { get; set; }
        public string UriAbsoluteDefault { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
    }
}