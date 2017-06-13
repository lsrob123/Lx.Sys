using Lx.Shared.All.Domains.Identity.Enumerations;
using Lx.Utilities.Contracts.Infrastructure.DTOs;

namespace Lx.Shared.All.Domains.Identity.DTOs
{
    public class CreateUserResponse : ResponseBase
    {
        public UserDtoBase User { get; set; }
        public UserUpdateResultType ResultType { get; set; }

        public override void EraseShareGroupInfoForClientSide()
        {
        }
    }
}