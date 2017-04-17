using System.Collections.Generic;

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