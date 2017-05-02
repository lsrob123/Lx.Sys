using System.Runtime.Serialization;
using Lx.Utilities.Contract.Infrastructure.Helpers;
using Lx.Utilities.Contract.Infrastructure.Interfaces;
using Newtonsoft.Json;

namespace Lx.Utilities.Contract.Infrastructure.DTOs {
    public class PhoneNumberDto : IDto, IPhoneNumber {
        public string LocalNumberWithAreaCode { get; set; }
        public int? CountryCode { get; set; }
        public string CountryName { get; set; }
        public bool Verified { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        public string FullNumber => this.GetFullNumber();

        public string LocalNumberWithAreaCodeInDigits => this.GetLocalNumberInDigits();

    }
}