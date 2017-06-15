using System.Collections.Generic;
using Lx.Utilities.Contracts.Infrastructure.DTOs;
using Newtonsoft.Json;

namespace Lx.Identity.Contracts.RequestsResponses
{
    public class ResetPasswordRequest : RequestBase
    {
        public string ResetPasswordMethod { get; set; }
        public EmailDto Email { get; set; }
        public PhoneNumberDto MobileNumber { get; set; }

        [JsonIgnore]
        public List<string> ValidationErrorMessages { get; set; }
    }
}