namespace Lx.Shared.All.Common.DTOs {
    public class PersonNameDto : IDto, IPersonName {
        public string FamilyName { get; set; }
        public string GivenName { get; set; }
        public string MiddleName { get; set; }
        public bool Verified { get; set; }
    }
}