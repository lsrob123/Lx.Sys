using System.Collections.Generic;
using System.Security.Claims;

namespace Lx.Utilities.Contract.Authentication {
    public interface IClaimProcessor {
        ICollection<Claim> Process(IEnumerable<Claim> claims);
    }
}