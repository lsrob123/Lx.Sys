using System.Collections.Generic;

namespace Lx.Identity.Contracts.DTOs
{
    public class ResetPasswordRequest : RequestBase
    {
        [EnumerationValidate(typeof (ResetPasswordMethod))]
        public string ResetPasswordMethod { get; set; }

        public EmailDto Email { get; set; }
        public PhoneNumberDto MobileNumber { get; set; }

        [JsonIgnore]
        public List<string> ValidationErrorMessages { get; set; }
    }
}