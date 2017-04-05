using Lx.Utilities.Contract.Infrastructure.Common;

namespace Lx.Utilities.Contract.Infrastructure.Enumerations {
    public class RichContentType : Enumeration {
        protected RichContentType(int value, string name) : base(value, name) { }

        protected RichContentType() { }

        public static RichContentType TextOnly => new RichContentType(0, nameof(TextOnly));
        public static RichContentType TextWithEmbeddedMedia => new RichContentType(10, nameof(TextWithEmbeddedMedia));
        public static RichContentType InternalReference => new RichContentType(20, nameof(InternalReference));
        public static RichContentType Url => new RichContentType(30, nameof(Url));
        public static RichContentType RichDocument => new RichContentType(40, nameof(RichDocument));
        public static RichContentType InlineHtml => new RichContentType(50, nameof(InlineHtml));
        public static RichContentType GraphicalImage => new RichContentType(60, nameof(GraphicalImage));
        public static RichContentType Audio => new RichContentType(80, nameof(Audio));
        public static RichContentType Video => new RichContentType(90, nameof(Video));
        public static RichContentType DataUri => new RichContentType(100, nameof(DataUri));
    }
}