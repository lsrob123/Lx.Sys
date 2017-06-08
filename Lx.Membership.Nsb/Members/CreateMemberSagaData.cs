using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus.Saga;

namespace Lx.Membership.Nsb.Members
{
   public class CreateMemberSagaData: ContainSagaData
    {
        public virtual string Email { get; set; }
        public virtual Guid UserKey { get; set; }
    }
}
