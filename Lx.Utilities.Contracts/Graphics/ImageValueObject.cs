using System;
using System.ComponentModel.DataAnnotations;
using Lx.Utilities.Contracts.Infrastructure.Domain;

namespace Lx.Utilities.Contracts.Graphics
{
    public class ImageValueObject : IValueObject, IImageFull
    {
        public Guid Key { get; protected set; }

        [StringLength(200)]
        public string FileName { get; protected set; }

        [StringLength(50)]
        public string FileNameExtension { get; protected set; }

        [StringLength(200)]
        public string RelativeUri { get; protected set; }

        [StringLength(500)]
        public string Uri { get; protected set; }

        [StringLength(500)]
        public string FullFilePath { get; protected set; }
    }
}