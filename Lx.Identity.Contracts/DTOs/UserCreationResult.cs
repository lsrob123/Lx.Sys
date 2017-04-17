using System;

namespace Lx.Identity.Contracts.DTOs
{
    public class UserCreationResult : IUserCreationResult
    {
        public string Username { get; set; }
        public EmailDto Email { get; set; }
        public PhoneNumberDto MobileNumber { get; set; }
        public Guid Key { get; set; }
        public UserState UserState { get; set; }
        public PersonNameDto Name { get; set; }
        public bool IsAdmin { get; set; }
        public UserCreationResultType ResultType { get; set; }
    }
}