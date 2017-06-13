using Lx.Utilities.Contracts.Infrastructure.Enumerations;

namespace Lx.Utilities.Contracts.Infrastructure.Interfaces
{
    public interface IHasRichContent
    {
        RichContentType RichContentType { get; }
        string TextContent { get; }
        string ThumbnailUrl { get; }
        string RichContentUrl { get; }
        string RichContentReference { get; }
    }
}