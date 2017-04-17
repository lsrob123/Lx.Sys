﻿using System.Collections.Generic;
using Lx.Shared.All.Common.DTOs;
using Lx.Utilities.Contract.Infrastructure.DTOs;
using Newtonsoft.Json;

namespace Lx.Identity.Contracts.DTOs
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