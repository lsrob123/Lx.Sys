using Lx.Utilities.Contract.Infrastructure.Common;

namespace Lx.Utilities.Contract.Enumerations.Common {
    public class RichContentType : Enumeration {
        public static readonly RichContentType PlainTextOnly = new RichContentType(0, nameof(PlainTextOnly));
        public static readonly RichContentType InternalReference = new RichContentType(10, nameof(InternalReference));
        public static readonly RichContentType RichDocument = new RichContentType(20, nameof(RichDocument));
        public static readonly RichContentType InlineHtml = new RichContentType(30, nameof(InlineHtml));
        public static readonly RichContentType GraphicalImage = new RichContentType(40, nameof(GraphicalImage));
        public static readonly RichContentType Audio = new RichContentType(50, nameof(Audio));
        public static readonly RichContentType Video = new RichContentType(60, nameof(Video));
        public static readonly RichContentType DataUri = new RichContentType(70, nameof(DataUri));

        public RichContentType(int value, string name) : base(value, name) {}

        public RichContentType() {}
    }
}