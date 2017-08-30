namespace Lx.Utilities.Contracts.Graphics
{
    public class ImageFullDto : ImageSafeDto, IImageFull
    {
        public string FullFilePath { get; set; }
    }
}