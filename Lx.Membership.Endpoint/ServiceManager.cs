﻿using Lx.Utilities.Services.WindowsService;

namespace Lx.Membership.Endpoint
{
    public class ServiceManager : ServiceManagerBase
    {
        public override void StartService()
        {
            StartEndpointWithStaticFileFolders("Assets");
        }
    }
}