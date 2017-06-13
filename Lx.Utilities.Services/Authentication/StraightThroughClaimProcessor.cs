using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Lx.Utilities.Contracts.Authentication.Interfaces;

namespace Lx.Utilities.Services.Authentication
{
    public class StraightThroughClaimProcessor : IClaimProcessor
    {
        public ICollection<Claim> Process(IEnumerable<Claim> claims)
        {
            return claims.ToList();
        }
    }
}