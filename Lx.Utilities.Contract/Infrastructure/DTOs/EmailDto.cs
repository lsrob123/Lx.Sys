﻿using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Utilities.Contract.Infrastructure.DTOs {
    public class EmailDto : IDto, IEmail {
        public string Name { get; set; } //composed from person name
        public string Address { get; set; }
        public bool Verified { get; set; }
    }
}