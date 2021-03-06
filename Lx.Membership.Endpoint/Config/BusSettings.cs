﻿using Lx.Utilities.Services.Config;
using Lx.Utilities.Services.ServiceBus.Nsb;

namespace Lx.Membership.Endpoint.Config
{
    public class BusSettings : NsbBusSettingsBase
    {
        public override string EndpointName => this.AppSettingStringValue(x => x.EndpointName);
    }
}