namespace Lx.Utilities.Contract.Graphics {
    public class ImageFullDto : ImageSafeDto, IImageFull {
        public string FullFilePath { get; set; }
    }
}