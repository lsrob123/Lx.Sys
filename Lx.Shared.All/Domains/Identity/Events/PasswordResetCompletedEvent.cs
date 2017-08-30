using Lx.Utilities.Contracts.Infrastructure.DTOs;

namespace Lx.Shared.All.Domains.Identity.Events
{
    public class PasswordResetCompletedEvent : EventBase
    {
        public override void EraseShareGroupInfoForClientSide()
        {
        }
    }
}