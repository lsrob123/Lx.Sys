using Lx.Shared.All.Identity.DTOs;
using Lx.Shared.All.Identity.Enumerations;

namespace Lx.Shared.All.Identity.Interfaces
{
    public interface IUserCreationResult : IUserDtoBase
    {
        UserCreationResultType ResultType { get; set; }
    }
}