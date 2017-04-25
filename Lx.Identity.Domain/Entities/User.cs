using System;
using System.ComponentModel.DataAnnotations;
using Lx.Identity.Contracts.Enumerations;
using Lx.Utilities.Contract.Authentication.Enumerations;
using Lx.Utilities.Contract.Infrastructure.Domain;
using Lx.Utilities.Contract.Infrastructure.Interfaces;
using Lx.Utilities.Contract.Infrastructure.ValueObjects;

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
            MobileNumber = mobileNumber;
            UserState = userState;
            IsAdmin = isAdmin;
        }

        [MaxLength(200)]
        public string HashedPassword { get; protected set; }

        public VerificationPurpose VerificationPurpose { get; protected set; }
        public ResetPasswordMethod ResetPasswordMethod { get; protected set; }

        [StringLength(1000)]
        public string HashedVerificationCode { get; protected set; }

        public DateTimeOffset? TimeVerificationCodeSent { get; protected set; }
        public DateTimeOffset? TimeVerificationCodeExpires { get; protected set; }
        public DateTimeOffset? TimeTemporaryPasswordSent { get; protected set; }
        public PriorUserState PriorUserState { get; protected set; }
        public DateTimeOffset? TimeLockedOut { get; protected set; }
        public UserState UserState { get; protected set; }
        public PersonName Name { get; protected set; }
        public bool IsAdmin { get; protected set; }

        [MaxLength(20)]
        public string Username { get; protected set; }

        public Email Email { get; protected set; }
        public PhoneNumber MobileNumber { get; protected set; }

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
            Name = new PersonName(value.FamilyName, value.GivenName, value.MiddleName);
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
            DateTimeOffset timeVerificationCodeExpires, DateTimeOffset? timeVerificationCodeSent = null)
        {
            VerificationPurpose = verificationPurpose;
            HashedVerificationCode = hashedVerificationCode;
            TimeVerificationCodeExpires = timeVerificationCodeExpires;
            TimeVerificationCodeSent = timeVerificationCodeSent;
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

        public VerificationResult ExecuteVerification(VerificationPurpose verificationPurpose,
            string hashedVerificationCode)
        {
            if (string.IsNullOrWhiteSpace(HashedVerificationCode) || !TimeVerificationCodeExpires.HasValue ||
                (VerificationPurpose != verificationPurpose))
                return VerificationResult.NewVerificationCodeRequired;

            if (!HashedVerificationCode.Equals(hashedVerificationCode, StringComparison.Ordinal))
                return VerificationResult.InvalidCode;

            var result = DateTimeOffset.UtcNow <= TimeVerificationCodeExpires.Value
                ? VerificationResult.Passed
                : VerificationResult.ExpiredCode;

            ResetVerificationCode();
            return result;
        }

        public User SetMobile(IPhoneNumber mobile)
        {
            MobileNumber = new PhoneNumber(mobile.LocalNumberWithAreaCode, mobile.Verified, mobile.CountryCode,
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
            Email = Email ?? new Email();
            MobileNumber = MobileNumber ?? new PhoneNumber();
            PriorUserState = PriorUserState ?? PriorUserState.Unknown;
            VerificationPurpose = VerificationPurpose ?? VerificationPurpose.Nothing;
            ResetPasswordMethod = ResetPasswordMethod ?? ResetPasswordMethod.Nothing;
            Name = Name ?? new PersonName();
        }
    }
}