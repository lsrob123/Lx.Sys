using Lx.Utilities.Contract.Infrastructure.Common;

namespace Lx.Utilities.Contract.Infrastructure.Enumerations {
    public class Locale : Enumeration {
        public static readonly Locale Unspecified = new Locale(0, nameof(Unspecified));
        public static readonly Locale English = new Locale(10, nameof(English));
        public static readonly Locale Chinese = new Locale(20, nameof(Chinese));
        public static readonly Locale Spanish = new Locale(30, nameof(Spanish));
        public static readonly Locale German = new Locale(40, nameof(German));

        protected Locale() {}

        protected Locale(int value, string name) : base(value, name) {}
    }
}