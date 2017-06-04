using System;
using System.ComponentModel.DataAnnotations;
using Lx.Shared.All.Domains.Identity.Interfaces;
using Lx.Utilities.Contract.Infrastructure.Domain;

namespace Lx.Identity.Domain.Entities {
    public class UserProfile : EntityBase, IUserProfile {
        [StringLength(100)]
        public string UserProfileOriginator { get; protected set; }

        public string Body { get; protected set; }
        public Guid UserKey { get; protected set; }

        public override void AssignDefaultValuesToComplexPropertiesIfNull() {}
    }
}