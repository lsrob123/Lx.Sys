using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Lx.Utilities.Services.Graphics
{
    public static class ImageExtension
    {
        public static ImageFormat ToImageFormat(this string obj)
        {
            if (string.IsNullOrWhiteSpace(obj))
                return null;

            switch (obj.ToLower())
            {
                case "bmp":
                    return ImageFormat.Bmp;
                case "emf":
                    return ImageFormat.Emf;
                case "exif":
                    return ImageFormat.Exif;
                case "gif":
                    return ImageFormat.Gif;
                case "icon":
                    return ImageFormat.Icon;
                case "jpeg":
                    return ImageFormat.Jpeg;
                case "jpg":
                    return ImageFormat.Jpeg;
                case "png":
                    return ImageFormat.Png;
                case "tiff":
                    return ImageFormat.Tiff;
                case "wmf":
                    return ImageFormat.Wmf;
                default:
                    return null;
            }
        }

        public static Image Resize(this Image sourceImage, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);

            using (var graphics = System.Drawing.Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(sourceImage, destRect, 0, 0, sourceImage.Width, sourceImage.Height,
                        GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static Image ToCroppedThumbnail(this Image sourceImage, int width)
        {
            var destImage = new Bitmap(width, width);
            using (var destGraphics = System.Drawing.Graphics.FromImage(destImage).WithHighQuality())
            {
                var isWidthGreaterThanHeight = sourceImage.Width > sourceImage.Height;
                var sideLengthMin = isWidthGreaterThanHeight ? sourceImage.Height : sourceImage.Width;
                var left = isWidthGreaterThanHeight ? (sourceImage.Width - sideLengthMin) / 2 : 0;
                var top = isWidthGreaterThanHeight ? 0 : (sourceImage.Height - sideLengthMin) / 2;
                destGraphics.DrawImage(sourceImage, new Rectangle(0, 0, width, width),
                    new Rectangle(left, top, sideLengthMin, sideLengthMin), GraphicsUnit.Pixel);
            }

            return destImage;
        }

        public static System.Drawing.Graphics WithHighQuality(this System.Drawing.Graphics graphics)
        {
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            return graphics;
        }
    }
}