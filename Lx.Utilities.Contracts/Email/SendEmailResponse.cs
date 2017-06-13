using System;
using Lx.Utilities.Contracts.Infrastructure.DTOs;

namespace Lx.Utilities.Contracts.Email
{
    public class SendEmailResponse : ResponseBase
    {
        public Guid BatchKey { get; set; }

        public override void EraseShareGroupInfoForClientSide()
        {
        }
    }
}