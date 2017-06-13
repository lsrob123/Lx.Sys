using Lx.Shared.All.Domains.Identity.ValueObjects;
using Lx.Utilities.Contracts.Authentication.Enumerations;
using Lx.Utilities.Contracts.Infrastructure.Domain;
using Lx.Utilities.Contracts.Infrastructure.ValueObjects;
using Lx.Utilities.Contracts.Membership.Enumerations;
using Lx.Utilities.Contracts.Membership.Interfaces;

namespace Lx.Membership.Domain.Entities
{
    public class Member : EntityBase, IMember<PersonName, Email, PhoneNumber, Address>
    {
        public UserState UserState { get; protected set; }
        public PersonName PersonName { get; protected set; }
        public bool IsAdmin { get; protected set; }
        public string Username { get; protected set; }
        public AccountState AccountState { get; protected set; }
        public string AvatarUriDefault { get; protected set; }
        public string AvatarUriRelative { get; protected set; }
        public string Nickname { get; protected set; }
        public Email Email { get; protected set; }
        public PhoneNumber Mobile { get; protected set; }
        public Address HomeAddress { get; protected set; }
        public Address WorkAddress { get; protected set; }
        public Address PostalAddress { get; protected set; }

        public override void AssignDefaultValuesToComplexPropertiesIfNull()
        {
            AccountState = AccountState ?? AccountState.Unknown;
            UserState = UserState ?? UserState.Unknown;
            Email = Email ?? new Email();
            Mobile = Mobile ?? new PhoneNumber();
            PersonName = PersonName ?? new PersonName();
            HomeAddress = HomeAddress ?? new Address();
            WorkAddress = WorkAddress ?? new Address();
            PostalAddress = PostalAddress ?? new Address();
        }
    }
}