using System;
using System.ComponentModel.DataAnnotations;
using Lx.Shared.All.Identity.Interfaces;
using Lx.Utilities.Contract.Infrastructure.Domain;

namespace Lx.Shared.All.Identity.ValueObjects
{
    public class Avatar : RelatedValueObjectBase, IAvatar
    {
        [StringLength(100)]
        public string UriRelative { get; protected set; }

        [StringLength(200)]
        public string UriAbsoluteDefault { get; protected set; }

        [StringLength(500)]
        public string Description { get; protected set; }

        public int? Width { get; protected set; }
        public int? Height { get; protected set; }
        public string FullFilePath { get; protected set; }
        public Guid UserKey { get; protected set; }
        public bool Deactivated { get; protected set; }
    }
}