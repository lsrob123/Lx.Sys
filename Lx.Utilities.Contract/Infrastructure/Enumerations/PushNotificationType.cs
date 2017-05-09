using Lx.Utilities.Contract.Infrastructure.Common;

namespace Lx.Utilities.Contract.Infrastructure.Enumerations {
    public class PushNotificationType : Enumeration {
        public static readonly PushNotificationType Device = new PushNotificationType(10, nameof(Device));
        public static readonly PushNotificationType Devices = new PushNotificationType(20, nameof(Devices));
        public static readonly PushNotificationType Topic = new PushNotificationType(30, nameof(Topic)); //not used
        public static readonly PushNotificationType Broadcast = new PushNotificationType(40, nameof(Broadcast));

        public PushNotificationType(int value, string name) : base(value, name) { }

        protected PushNotificationType() { }
    }
}