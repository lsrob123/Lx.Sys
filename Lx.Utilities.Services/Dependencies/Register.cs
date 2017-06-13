using Lx.Utilities.Contracts.Authentication.Config;
using Lx.Utilities.Contracts.Authentication.Interfaces;
using Lx.Utilities.Contracts.Authorization;
using Lx.Utilities.Contracts.Caching;
using Lx.Utilities.Contracts.Crypto;
using Lx.Utilities.Contracts.Infrastructure.EventBroadcasting;
using Lx.Utilities.Contracts.Infrastructure.RequestDispatching;
using Lx.Utilities.Contracts.IoC;
using Lx.Utilities.Contracts.Logging;
using Lx.Utilities.Contracts.Mapping;
using Lx.Utilities.Contracts.Mediator;
using Lx.Utilities.Contracts.Serialization;
using Lx.Utilities.Services.Authentication;
using Lx.Utilities.Services.Authorization;
using Lx.Utilities.Services.Caching.InProcess;
using Lx.Utilities.Services.Caching.Redis;
using Lx.Utilities.Services.Crypto;
using Lx.Utilities.Services.Infrastructure;
using Lx.Utilities.Services.Logging.Log4Net;
using Lx.Utilities.Services.Mapping.AutoMapper;
using Lx.Utilities.Services.Serialization;
using Lx.Utilities.Services.SignalR;

namespace Lx.Utilities.Services.Dependencies
{
    public class Register : DefaultDependencyRegisterBase
    {
        public override void AddRegistrations()
        {
            Register<ICryptoService, CryptoService>();
            Register(() => Mediator.Default);
            Register<ICryptoService, CryptoService>();
            Register<ILogger>(() => new Logger());
            Register<IMappingService>(() => new MappingService());
            Register<ISerializer>(() => new JsonSerializer());
            Register<IClaimRelatedJsonDeserializer>(() => new ClaimRelatedJsonDeserializer());
            Register<ICacheFactory, CacheFactory>(externallyOwned: true, singleInstance: true);
            Register<ISignalRConfig, SignalRConfig>();
            Register<IInProcessCache, InProcessCache>();
            Register<IOAuthHelper, OAuthHelper>();
            Register<IAuthorizationService, AuthorizationService>();
            Register<IOAuthClientSettings, OAuthClientSettings>();
            Register<IRequestDispatchingProxy, RequestDispatchingProxy>();
            Register<IEventBroadcastingProxy, EventBroadcastingProxy>();
            Register<IOAuthClientService, OAuthClientService>();
        }
    }
}