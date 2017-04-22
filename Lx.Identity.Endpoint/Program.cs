using Lx.Utilities.Services.WindowsService.Topshelf;
using Topshelf;

namespace Lx.Identity.Endpoint
{
    internal class Program
    {
        public static Host Host { get; protected set; }

        private static void Main(string[] args)
        {
            Host = new ServiceHostInitializer<ServiceManager>().Host;
        }
    }
}