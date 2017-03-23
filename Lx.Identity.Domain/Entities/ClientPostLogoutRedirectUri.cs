using System.ComponentModel.DataAnnotations;
using Lx.Identity.Contracts.Interfaces;
using Lx.Utilities.Contract.Infrastructure.Domain;

namespace Lx.Identity.Domain.Entities {
    public class ClientPostLogoutRedirectUri : EntityBase, IClientPostLogoutRedirectUri {
        [StringLength(2000)]
        public string Uri { get; protected set; }

        public override void AssignDefaultValuesToComplexPropertiesIfNull() {}
    }
}