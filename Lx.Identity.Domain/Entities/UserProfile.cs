using System;
using System.ComponentModel.DataAnnotations;
using Lx.Identity.Contracts.Interfaces;
using Lx.Utilities.Contract.Infrastructure.Domain;

namespace Lx.Identity.Domain.Entities {
    public class UserProfile : EntityBase, IUserProfile {
        [StringLength(100)]
        public string Context { get; protected set; }

        [StringLength(100)]
        public string Group { get; protected set; }

        public string Body { get; protected set; }
        public Guid UserKey { get; protected set; }

        public override void AssignDefaultValuesToComplexPropertiesIfNull() {}
    }
}