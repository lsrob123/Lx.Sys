using System.Drawing;
using Lx.Utilities.Contracts.Graphics;

namespace Lx.Utilities.Services.Graphics
{
    public class ThumbnailProcessor : IImageProcessor
    {
        protected readonly int Width;

        public ThumbnailProcessor(int width)
        {
            Width = width;
        }

        public void Process(string sourceImageFileFullPath, string destImageFileFullPath)
        {
            var image = Image.FromFile(sourceImageFileFullPath).ToCroppedThumbnail(Width);
            image.Save(destImageFileFullPath);
        }
    }
}