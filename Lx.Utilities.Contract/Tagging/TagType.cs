using Lx.Utilities.Contract.Infrastructure.Common;

namespace Lx.Utilities.Contract.Tagging {
    public class TagType : Enumeration {
        protected TagType() {}

        protected TagType(int value, string name, string sign) : base(value, name) {
            Sign = sign;
        }

        public string Sign { get; set; }

        public static TagType Unknown => new TagType(0, nameof(Unknown), string.Empty);
        public static TagType OnTopic => new TagType(10, nameof(OnTopic), "#");
        public static TagType OnIdentity => new TagType(20, nameof(OnIdentity), "@");
    }
}