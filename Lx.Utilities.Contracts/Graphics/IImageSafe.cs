using System;

namespace Lx.Utilities.Contracts.Graphics
{
    public interface IImageSafe
    {
        Guid Key { get; }
        string FileName { get; }
        string FileNameExtension { get; }
        string RelativeUri { get; }
        string Uri { get; }
    }
}