﻿using Lx.Utilities.Contract.Infrastructure.DTOs;

namespace Lx.Shared.All.Identity.DTOs {
    public class UpdateUserResponse : ResponseBase {
        public override void EraseShareGroupInfoForClientSide() {
        }
    }
}