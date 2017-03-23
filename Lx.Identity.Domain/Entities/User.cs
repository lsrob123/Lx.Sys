using System;
using System.ComponentModel.DataAnnotations;
using Lx.Identity.Contracts.Enumerations;
using Lx.Utilities.Contract.Authentication.Enumerations;
using Lx.Utilities.Contract.Infrastructure.Domain;
using Lx.Utilities.Contract.Infrastructure.ValueObjects;

namespace Lx.Identity.Domain.Entities {
    public class User : EntityBase {
        [MaxLength(200)]
        public string HashedPassword { get; protected set; }

        public VerificationPurpose VerificationPurpose { get; protected set; }
        public ResetPasswordMethod ResetPasswordMethod { get; protected set; }

        [StringLength(150)]
        public string VerficationCode { get; protected set; }

        public DateTimeOffset? VerificationCodeSent { get; protected set; }
        public DateTimeOffset? TemporaryPasswordSent { get; protected set; }
        public PriorUserState PriorUserState { get; protected set; }
        public DateTimeOffset? TimeLockedOut { get; protected set; }
        public UserState UserState { get; protected set; }

        public PersonName Name { get; protected set; }

        public bool IsAdmin { get; protected set; }

        [MaxLength(20)]
        public string Username { get; protected set; }

        public Email Email { get; protected set; }

        public PhoneNumber MobileNumber { get; protected set; }

        public void SetAdminRole(bool isAdmin) {
            IsAdmin = isAdmin;
        }

        public User SetUserName(string username) {
            Username = username;
            return this;
        }

        public User SetUserState(UserState state) {
            UserState = state;
            return this;
        }

        public User SetHashedPassword(string hashedPassword) {
            HashedPassword = hashedPassword;
            return this;
        }

        public User SetVerificationCode(string hashedCode) {
            VerficationCode = hashedCode;
            return this;
        }

        public User SetVerificationCodeSendTime(DateTimeOffset sendTime) {
            VerificationCodeSent = sendTime;
            return this;
        }

        public User SetVerificationPurpose(VerificationPurpose verificationPurpose) {
            VerificationPurpose = verificationPurpose;
            return this;
        }

        public User SetResetPasswordMethod(ResetPasswordMethod resetPasswordMethod) {
            ResetPasswordMethod = resetPasswordMethod;
            return this;
        }

        public User SetTemporaryPasswordSendTime(DateTimeOffset sendTime) {
            TemporaryPasswordSent = sendTime;
            return this;
        }

        public void LockOut() {
            TimeLockedOut = DateTimeOffset.Now;
            PriorUserState = UserState;
            UserState = UserState.LockedOut;
        }

        public void Unlock() {
            TimeLockedOut = null;
            UserState = PriorUserState;
        }

        public override void AssignDefaultValuesToComplexPropertiesIfNull() {
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