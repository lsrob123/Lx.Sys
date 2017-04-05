﻿using System;
using Lx.Identity.Contracts.Interfaces;

namespace Lx.Identity.Contracts.DTOs {
    public class ScopeSecretDto : IScopeSecret {
        public string Description { get; set; }
        public DateTimeOffset? Expiration { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}