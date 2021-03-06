﻿using System.Threading.Tasks;
using Lx.Utilities.Contract.Authentication.DTOs;

namespace Lx.Utilities.Contract.Authentication.Interfaces {
    public interface IOAuthHelper {
        Task<IdentityDto> GetUserAsync(string accessToken);
    }
}