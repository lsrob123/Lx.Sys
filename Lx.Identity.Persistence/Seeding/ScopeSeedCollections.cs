using System;
using System.Collections.Generic;
using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using Lx.Identity.Contracts.DTOs;
using Lx.Identity.Domain.Entities;
using Lx.Utilities.Contract.Mapping;
using Lx.Utilities.Services.Mapping.AutoMapper;
using Scope = Lx.Identity.Domain.Entities.Scope;
using ScopeClaim = Lx.Identity.Domain.Entities.ScopeClaim;

namespace Lx.Identity.Persistence.Seeding {
    public static class ScopeSeedCollections {
        public static IMappingService MappingService = new MappingService();
        public static readonly Guid ScopeKey = new Guid("7b3f8816-a621-4abc-bacf-117b8479cb38");

        public static ICollection<Scope> Scopes() {
            return new List<Scope> {
                MappingService.Map<Scope>(new ScopeDto {
                    Key = ScopeKey,
                    Enabled = true,
                    Name = "TestScope",
                    IncludeAllClaimsForUser = true
                })
            };
        }

        public static ICollection<ScopeClaim> ScopeClaims() {
            return new List<ScopeClaim> {
                MappingService.Map<ScopeClaim>(new ScopeClaimDto {
                    Key = new Guid("c7be10dc-cd19-4297-a271-7a9de48184ab"),
                    AlwaysIncludeInIdToken = true,
                    Name = Constants.ClaimTypes.Email,
                    ScopeKey = ScopeKey
                }),
                MappingService.Map<ScopeClaim>(new ScopeClaimDto {
                    Key = new Guid("1ee6190b-4270-4e55-9bf1-49dc5022c7a2"),
                    AlwaysIncludeInIdToken = true,
                    Name = Constants.ClaimTypes.PhoneNumber,
                    ScopeKey = ScopeKey
                }),
                MappingService.Map<ScopeClaim>(new ScopeClaimDto {
                    Key = new Guid("0ed8c57a-c533-4321-8cba-7de8f592a182"),
                    AlwaysIncludeInIdToken = true,
                    Name = Constants.ClaimTypes.Profile,
                    ScopeKey = ScopeKey
                })
            };
        }

        public static ICollection<ScopeSecret> ScopeSecrets() {
            return new List<ScopeSecret> {
                MappingService.Map<ScopeSecret>(new ScopeSecretDto {
                    Key = new Guid("474f23b7-f7ae-4da2-a38e-150e6c35341e"),
                    Value = "456".Sha512(),
                    Type = Constants.SecretTypes.SharedSecret,
                    ScopeKey = ScopeKey
                })
            };
        }
    }
}