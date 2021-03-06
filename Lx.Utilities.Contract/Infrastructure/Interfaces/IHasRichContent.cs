﻿using Lx.Utilities.Contract.Infrastructure.Enumerations;

namespace Lx.Utilities.Contract.Infrastructure.Interfaces {
    public interface IHasRichContent {
        RichContentType RichContentType { get; }
        string TextContent { get; }
        string ThumbnailUrl { get; }
        string RichContentUrl { get; }
        string RichContentReference { get; }
    }
}