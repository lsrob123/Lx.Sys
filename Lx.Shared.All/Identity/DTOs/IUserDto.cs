using System;
using Lx.Utilities.Contract.Authentication.Enumerations;

namespace Lx.Shared.All.Identity.DTOs {
    public interface IUserDto : IUserDtoBase {
        string HashedPassword { get; set; }
        VerificationPurpose VerificationPurpose { get; set; }
        string VerficationCode { get; set; }
        PriorUserState PriorUserState { get; set; }
        DateTimeOffset? TimeLockedOut { get; set; }
        UserProfileDto UserProfile { get; set; }
    }
}