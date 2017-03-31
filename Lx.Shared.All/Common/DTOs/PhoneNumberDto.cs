﻿namespace Lx.Shared.All.Common.DTOs {
    public class PhoneNumberDto : IDto, IPhoneNumber {
        public string LocalNumberWithAreaCode { get; set; }
        public int? CountryCode { get; set; }
        public string CountryName { get; set; }
        public bool Verified { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        public string FullNumber => PhoneNumberHelper.GetFullNumber(this);
    }
}