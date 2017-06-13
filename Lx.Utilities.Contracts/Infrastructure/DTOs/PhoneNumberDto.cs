using System.Runtime.Serialization;
using Lx.Utilities.Contracts.Infrastructure.Helpers;
using Lx.Utilities.Contracts.Infrastructure.Interfaces;
using Newtonsoft.Json;

namespace Lx.Utilities.Contracts.Infrastructure.DTOs
{
    public class PhoneNumberDto : IDto, IPhoneNumber
    {
        [IgnoreDataMember]
        [JsonIgnore]
        public string LocalNumberWithAreaCodeInDigits => this.GetLocalNumberInDigits();

        public string LocalNumberWithAreaCode { get; set; }
        public int? CountryCode { get; set; }
        public string CountryName { get; set; }
        public bool Verified { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        public string FullNumber => this.GetFullNumber();
    }
}