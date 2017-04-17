using Lx.Utilities.Contract.Infrastructure.Common;

namespace Lx.Utilities.Contract.Authentication.Enumerations
{
    public class PriorUserState : Enumeration
    {
        public PriorUserState(int value, string name) : base(value, name)
        {
        }

        protected PriorUserState()
        {
        }

        public static PriorUserState Unknown => new PriorUserState(0, nameof(Unknown));
        public static PriorUserState Active => new PriorUserState(10, nameof(Active));
        public static PriorUserState LockedOut => new PriorUserState(40, nameof(LockedOut));
        public static PriorUserState Deactivated => new PriorUserState(50, nameof(Deactivated));

        public static implicit operator UserState(PriorUserState priorUserState)
        {
            return new UserState(priorUserState.Value, priorUserState.Name);
        }
    }
}