using System.Collections.Generic;
using Lx.Utilities.Contracts.Authentication.Enumerations;
using Lx.Utilities.Contracts.Infrastructure.DTOs;
using Newtonsoft.Json;

namespace Lx.Identity.Contracts.DTOs
{
    public class CreateVerificationCodeRequest : RequestBase
    {
        public VerificationPurpose VerificationPurpose { get; set; }
        public EmailDto Email { get; set; }
        public PhoneNumberDto MobileNumber { get; set; }

        [JsonIgnore]
        public List<string> ValidationErrorMessages { get; set; }
    }
}