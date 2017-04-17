using Lx.Utilities.Contract.WindowsService;
using Lx.Utilities.Services.Config;

namespace Lx.Utilities.Services.WindowsService
{
    public abstract class ServiceManagerBase : IServiceManager
    {
        /// <summary>
        ///     ServiceManagerBase
        /// </summary>
        /// <param name="explicitlyApplyGlobalPreConfiguration">
        ///     Defaulted to false as Autofac builder extension CallDefaultDependencyRegisters()
        ///     will also call Preconfigurator.Configure() to avoid null reference exceptions
        ///     when registering pre-configured instances.
        /// </param>
        /// <remarks>
        ///     Preconfigurator.Configure() is thread safe and idempotent
        /// </remarks>
        protected ServiceManagerBase(bool explicitlyApplyGlobalPreConfiguration = false)
        {
            if (explicitlyApplyGlobalPreConfiguration)
                Preconfigurator.Configure();
        }

        public abstract void StartService();
        public abstract void StopService();
    }
}