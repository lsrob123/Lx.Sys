using Lx.Utilities.Contract.Infrastructure.Common;

namespace Lx.Utilities.Contract.Authentication.Enumerations {
    public class UserState : Enumeration {
        public UserState(int value, string name) : base(value, name) { }

        protected UserState() { }

        public static UserState Unknown => new UserState(0, nameof(Unknown));
        public static UserState Active => new UserState(10, nameof(Active));
        public static UserState LockedOut => new UserState(20, nameof(LockedOut));
        public static UserState Deactivated => new UserState(30, nameof(Deactivated));
        public static UserState Pending => new UserState(40, nameof(Pending));
        public static UserState Failed => new UserState(50, nameof(Failed));
        public static UserState Banned => new UserState(60, nameof(Banned));
        public static UserState MemberInfoNotFound => new UserState(70, nameof(MemberInfoNotFound));

        public static implicit operator PriorUserState(UserState userState) {
            return new PriorUserState(userState.Value, userState.Name);
        }
    }
}