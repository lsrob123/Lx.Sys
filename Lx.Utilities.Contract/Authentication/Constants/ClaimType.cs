namespace Lx.Utilities.Contract.Authentication.Constants
{
    public static class ClaimType
    {
        public const string Subject = "sub",
            IsAuthorized = "is_authorized",
            Profile = "profile",
            Role = "role",
            Email = "email",
            VerifiedEmail = "email_verified",
            PhoneNumber = "phone_number",
            VerifiedPhoneNumber = "phone_number_verified";
    }
}