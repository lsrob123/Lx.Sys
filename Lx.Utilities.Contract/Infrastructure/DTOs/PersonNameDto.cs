﻿using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Utilities.Contract.Infrastructure.DTOs {
    public class PersonNameDto : IDto, IPersonName {
        public string FamilyName { get; set; }
        public string GivenName { get; set; }
        public string MiddleName { get; set; }
        public bool Verified { get; set; }
    }
}