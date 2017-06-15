﻿using Lx.Membership.Persistence.UowFactories;
using Lx.Membership.Services.Processes;
using Lx.Utilities.Contracts.IoC;

namespace Lx.Membership.Services.Dependencies
{
    public class Register : DefaultDependencyRegisterBase
    {
        public override void AddRegistrations()
        {
            Register<IMembershipUowFactory, MembershipUowFactory>();

            Register<IMemberService, MemberService>();
        }
    }
}