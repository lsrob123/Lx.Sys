using System;
using Lx.Utilities.Contracts.Infrastructure.Interfaces;

namespace Lx.Utilities.Contracts.Graphics
{
    public class ImageSafeDto : IDto, IImageSafe
    {
        public Guid Key { get; set; }
        public string FileName { get; set; }
        public string FileNameExtension { get; set; }
        public string RelativeUri { get; set; }
        public string Uri { get; set; }
    }
}