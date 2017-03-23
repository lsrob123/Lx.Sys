using Lx.Utilities.Contract.Infrastructure.Common;

namespace Lx.Utilities.Contract.Enumerations.Identity {
    public class PriorUserState : Enumeration {
        public static readonly PriorUserState Unknown = new PriorUserState(0, nameof(Unknown));
        public static readonly PriorUserState Active = new PriorUserState(10, nameof(Active));
        public static readonly PriorUserState LockedOut = new PriorUserState(40, nameof(LockedOut));
        public static readonly PriorUserState Deactivated = new PriorUserState(50, nameof(Deactivated));

        public PriorUserState(int value, string name) : base(value, name) {}

        protected PriorUserState() //: this(Unknown.Value, Unknown.Name)
        {}

        public static implicit operator UserState(PriorUserState priorUserState) {
            return new UserState(priorUserState.Value, priorUserState.Name);
        }
    }
}