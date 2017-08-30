using System;

namespace Lx.Utilities.Contracts.Graphics
{
    public interface IAttachedImage<out TImage> where TImage : IImageSafe
    {
        Guid OwnerObjectKey { get; }
        int DisplayOrder { get; }
        TImage Image { get; }
        Guid Key { get; }
    }
}