using Lx.Utilities.Services.WindowsService;

namespace Lx.Membership.Endpoint
{
    public class ServiceManager : ServiceManagerBase
    {
        public ServiceManager() : base(true)
        {
        }

        public override void StartService()
        {
            StartEndpointWithStaticFileFolders("Assets");
        }
    }
}