using Lx.Utilities.Services.WindowsService;

namespace Lx.Communications.Endpoint
{
    public class ServiceManager : ServiceManagerBase
    {
        public override void StartService()
        {
            StartEndpointWithStaticFileFolders("Assets");
        }
    }
}