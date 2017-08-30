using System;
using Lx.Utilities.Contract.Infrastructure.DTOs;

namespace Lx.Utilities.Contract.Email
{
    public class SendEmailResponse : ResponseBase
    {
        public Guid BatchKey { get; set; }

        public override void EraseShareGroupInfoForClientSide()
        {
        }
    }
}