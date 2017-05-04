using Topshelf;

namespace Lx.Utilities.Services.WindowsService.Topshelf
{
    public abstract class ProgramBase
    {
        protected static Host Host { get; set; }
    }

    public abstract class ProgramBase<TServiceManager> : ProgramBase
        where TServiceManager : ServiceManagerBase, new()
    {
        protected static void Init()
        {
            Host = new ServiceHostInitializer<TServiceManager>().Host;
        }
    }
}