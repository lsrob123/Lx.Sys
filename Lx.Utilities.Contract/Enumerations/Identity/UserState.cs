using Lx.Utilities.Contract.Infrastructure.Common;

namespace Lx.Utilities.Contract.Enumerations.Identity {
    public class UserState : Enumeration {
        public static readonly UserState Unknown = new UserState(0, nameof(Unknown));
        public static readonly UserState Active = new UserState(10, nameof(Active));
        public static readonly UserState LockedOut = new UserState(20, nameof(LockedOut));
        public static readonly UserState Deactivated = new UserState(30, nameof(Deactivated));
        public static readonly UserState Pending = new UserState(40, nameof(Pending));
        public static readonly UserState Failed = new UserState(50, nameof(Failed));
        public static readonly UserState Banned = new UserState(60, nameof(Banned));
        public static readonly UserState MemberInfoNotFound = new UserState(70, nameof(MemberInfoNotFound));

        public UserState(int value, string name) : base(value, name) {}

        protected UserState() //: this(Unknown.Value, Unknown.Name)
        {}

        public static implicit operator PriorUserState(UserState userState) {
            return new PriorUserState(userState.Value, userState.Name);
        }
    }
}