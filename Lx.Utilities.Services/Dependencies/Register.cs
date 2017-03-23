using Lx.Utilities.Contract.Authentication;
using Lx.Utilities.Contract.Authentication.Config;
using Lx.Utilities.Contract.Authorization;
using Lx.Utilities.Contract.Caching;
using Lx.Utilities.Contract.Crypto;
using Lx.Utilities.Contract.Infrastructure.Common;
using Lx.Utilities.Contract.IoC;
using Lx.Utilities.Contract.Logging;
using Lx.Utilities.Contract.Mapping;
using Lx.Utilities.Contract.Serialization;
using Lx.Utilities.Services.Authentication;
using Lx.Utilities.Services.Authorization;
using Lx.Utilities.Services.Caching.InProcess;
using Lx.Utilities.Services.Caching.Redis;
using Lx.Utilities.Services.Crypto;
using Lx.Utilities.Services.Logging.Log4Net;
using Lx.Utilities.Services.Mapping.AutoMapper;
using Lx.Utilities.Services.Serialization;
using Lx.Utilities.Services.ServiceBus.NSB;
using Lx.Utilities.Services.SignalR;

namespace Lx.Utilities.Services.Dependencies {
    public class Register : DefaultDependencyRegisterBase {
        public override void AddRegistrations() {
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
            Register<IRequestDispatcher, RequestDispatcher>();
        }
    }
}