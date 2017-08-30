using System;
using System.ComponentModel.DataAnnotations;
using Lx.Identity.Contracts.Enumerations;
using Lx.Shared.All.Domains.Identity.ValueObjects;
using Lx.Utilities.Contracts.Authentication.Enumerations;
using Lx.Utilities.Contracts.Infrastructure.Domain;
using Lx.Utilities.Contracts.Infrastructure.Interfaces;
using Lx.Utilities.Contracts.Infrastructure.ValueObjects;

namespace Lx.Identity.Domain.Entities
{
    public class User : EntityBase, IUser
    {
        public User() : this(Guid.Empty, null, null, new Email(), new PhoneNumber(), UserState.Unknown)
        {
        }

        public User(Guid key, string username, string hashedPassword, Email email, PhoneNumber mobileNumber,
            UserState userState, bool isAdmin = false) : base(key)
        {
            Username = username;
            HashedPassword = hashedPassword;
            Email = email;
            Mobile = mobileNumber;
            UserState = userState;
            IsAdmin = isAdmin;
        }

        [MaxLength(200)]
        public string HashedPassword { get; protected set; }

        public VerificationPurpose VerificationPurpose { get; protected set; }

        /// <summary>
        /// TODO: Review to see if really needed
        /// </summary>
        public ResetPasswordMethod ResetPasswordMethod { get; protected set; }

        [StringLength(1000)]
        public string HashedVerificationCode { get; protected set; }

        public DateTimeOffset? TimeVerificationCodeSent { get; protected set; }
        public DateTimeOffset? TimeVerificationCodeExpires { get; protected set; }
        public DateTimeOffset? TimeTemporaryPasswordSent { get; protected set; }
        public PriorUserState PriorUserState { get; protected set; }
        public DateTimeOffset? TimeLockedOut { get; protected set; }
        public UserState UserState { get; protected set; }
        public PersonName PersonName { get; protected set; }
        public bool IsAdmin { get; protected set; }

        [MaxLength(20)]
        public string Username { get; protected set; }

        public Email Email { get; protected set; }
        public PhoneNumber Mobile { get; protected set; }
        public Address HomeAddress { get; protected set; }
        public Address WorkAddress { get; protected set; }
        public Address PostalAddress { get; protected set; }

        public void SetAdminRole(bool isAdmin)
        {
            IsAdmin = isAdmin;
        }

        public User SetUserName(string username)
        {
            Username = username;
            return this;
        }

        public User SetPersonName(IPersonName value)
        {
            PersonName = new PersonName(value.FamilyName, value.GivenName, value.MiddleName);
            return this;
        }

        public User SetUserState(UserState state)
        {
            UserState = state;
            return this;
        }

        public User SetHashedPassword(string hashedPassword)
        {
            HashedPassword = hashedPassword;
            return this;
        }

        public User WithVerificationCode(VerificationPurpose verificationPurpose, string hashedVerificationCode,
            DateTimeOffset timeVerificationCodeExpires)
        {
            VerificationPurpose = verificationPurpose;
            HashedVerificationCode = hashedVerificationCode;
            TimeVerificationCodeExpires = timeVerificationCodeExpires;
            return this;
        }

        public User ResetVerificationCode()
        {
            VerificationPurpose = VerificationPurpose.Nothing;
            HashedVerificationCode = null;
            TimeVerificationCodeExpires = null;
            TimeVerificationCodeSent = null;
            return this;
        }

        public User SetMobile(IPhoneNumber mobile)
        {
            Mobile = new PhoneNumber(mobile.LocalNumberWithAreaCode, mobile.Verified, mobile.CountryCode,
                mobile.CountryName);
            return this;
        }

        public User SetVerificationPurpose(VerificationPurpose verificationPurpose)
        {
            VerificationPurpose = verificationPurpose;
            return this;
        }

        public User SetResetPasswordMethod(ResetPasswordMethod resetPasswordMethod)
        {
            ResetPasswordMethod = resetPasswordMethod;
            return this;
        }

        public User SetTemporaryPasswordSendTime(DateTimeOffset sendTime)
        {
            TimeTemporaryPasswordSent = sendTime;
            return this;
        }

        public void LockOut()
        {
            TimeLockedOut = DateTimeOffset.Now;
            PriorUserState = UserState;
            UserState = UserState.LockedOut;
        }

        public void Unlock()
        {
            TimeLockedOut = null;
            UserState = PriorUserState;
        }

        public override void AssignDefaultValuesToComplexPropertiesIfNull()
        {
            UserState = UserState ?? UserState.Unknown;
            PriorUserState = PriorUserState ?? PriorUserState.Unknown;
            VerificationPurpose = VerificationPurpose ?? VerificationPurpose.Nothing;
            ResetPasswordMethod = ResetPasswordMethod ?? ResetPasswordMethod.Nothing;
            PersonName = PersonName ?? new PersonName();
            Email = Email ?? new Email();
            Mobile = Mobile ?? new PhoneNumber();
            HomeAddress = HomeAddress ?? new Address();
            WorkAddress = WorkAddress ?? new Address();
            PostalAddress = PostalAddress ?? new Address();
        }
    }
}