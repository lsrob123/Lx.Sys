using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using Lx.Utilities.Contracts.Caching;
using Lx.Utilities.Contracts.Logging;
using Lx.Utilities.Contracts.Serialization;

namespace Lx.Identity.ID3.Stores
{
    public class Id3TokenHandleStore : Id3TokenStoreBase<Token>, ITokenHandleStore
    {
        public Id3TokenHandleStore(ICacheFactory cacheFactory, ISerializer serializer, ILogger logger,
            IClaimRelatedJsonDeserializer claimRelatedJsonDeserializer)
            : base(cacheFactory, serializer, logger, claimRelatedJsonDeserializer)
        {
        }
    }
}