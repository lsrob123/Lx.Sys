using System;
using NServiceBus.Saga;

namespace Lx.Membership.Nsb.Members
{
    public class CreateMemberSagaData : ContainSagaData
    {
        public virtual string Email { get; set; }
        public virtual Guid UserKey { get; set; }
    }
}