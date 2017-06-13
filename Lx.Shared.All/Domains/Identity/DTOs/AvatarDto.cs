using System;
using Lx.Shared.All.Domains.Identity.Interfaces;
using Lx.Utilities.Contracts.Infrastructure.Interfaces;

namespace Lx.Shared.All.Domains.Identity.DTOs
{
    public class AvatarDto : IDto, IAvatar
    {
        public Guid UserKey { get; set; }
        public bool Deactivated { get; set; }
        public string Description { get; set; }
        public string FullFilePath { get; set; }
        public string UriRelative { get; set; }
        public string UriDefault { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
    }
}