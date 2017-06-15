using System.Collections.Generic;
using Lx.Utilities.Contracts.Infrastructure.DTOs;
using Newtonsoft.Json;

namespace Lx.Identity.Contracts.RequestsResponses
{
    public class ResetPasswordRequest : RequestBase
    {
        public EmailDto Email { get; set; }
    }
}