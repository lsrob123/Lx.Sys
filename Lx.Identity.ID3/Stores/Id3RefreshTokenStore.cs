using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using Lx.Utilities.Contract.Caching;
using Lx.Utilities.Contract.Logging;
using Lx.Utilities.Contract.Serialization;

namespace Lx.Identity.ID3.Stores
{
    public class Id3RefreshTokenStore : Id3TokenStoreBase<RefreshToken>, IRefreshTokenStore
    {
        public Id3RefreshTokenStore(ICacheFactory cacheFactory, ISerializer serializer, ILogger logger,
            IClaimRelatedJsonDeserializer claimRelatedJsonDeserializer)
            : base(cacheFactory, serializer, logger, claimRelatedJsonDeserializer)
        {
        }
    }
}