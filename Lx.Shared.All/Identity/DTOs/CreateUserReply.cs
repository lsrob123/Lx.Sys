using System;
using Lx.Shared.All.Common.DTOs;
using Lx.Shared.All.Identity.Enumerations;
using Lx.Shared.All.Identity.Interfaces;
using Lx.Utilities.Contract.Authentication.Enumerations;
using Lx.Utilities.Contract.Infrastructure.DTOs;

namespace Lx.Shared.All.Identity.DTOs {
    public class CreateUserReply : ReplyBase, IUserCreationResult {
        public virtual UserCreationResultType ResultType { get; set; }
        public virtual string Username { get; set; }
        public virtual EmailDto Email { get; set; }
        public virtual PhoneNumberDto MobileNumber { get; set; }
        public virtual Guid Key { get; set; }
        public virtual UserState UserState { get; set; }
        public PersonNameDto Name { get; set; }
        public bool IsAdmin { get; set; }
        public override void EraseShareGroupInfoForClientSide() {
        }
    }
}