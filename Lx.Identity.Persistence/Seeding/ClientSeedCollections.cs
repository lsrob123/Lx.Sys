using System;
using System.Collections.Generic;
using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using Lx.Identity.Contracts.DTOs;
using Lx.Identity.Domain.Entities;
using Lx.Shared.All.Identity.Config;
using Lx.Utilities.Contract.Mapping;
using Lx.Utilities.Services.Mapping.AutoMapper;
using AccessTokenType = Lx.Identity.Contracts.Enumerations.AccessTokenType;
using Client = Lx.Identity.Domain.Entities.Client;
using Flows = Lx.Identity.Contracts.Enumerations.Flows;
using TokenExpiration = Lx.Identity.Contracts.Enumerations.TokenExpiration;
using TokenUsage = Lx.Identity.Contracts.Enumerations.TokenUsage;

namespace Lx.Identity.Persistence.Seeding {
    public static class ClientSeedCollections {
        private const string ClientId = "TestClient1";
        private static readonly Guid ClientKey = new Guid("c6c179be-fd92-4c41-a192-4d6c20de2e0e");
        public static IMappingService MappingService = new MappingService();

        public static ICollection<Client> Clients() {
            var client1Dto = new ClientDto {
                Key = ClientKey,
                UserProfileOriginator = new UserProfileConfig().UserProfileOriginator,
                Enabled = true,
                ClientId = ClientId,
                ClientName = "test client 01",
                Flow = Flows.ResourceOwner,
                AccessTokenLifetime = 600,
                IdentityTokenLifetime = 600,
                AccessTokenType = AccessTokenType.Reference,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                AbsoluteRefreshTokenLifetime = 86400,
                SlidingRefreshTokenLifetime = 3600,
                AllowAccessToAllGrantTypes = true,
                AllowRememberConsent = true,
                EnableLocalLogin = true,
                RefreshTokenUsage = TokenUsage.OneTimeOnly
            };
            var clients = new List<Client> {
                MappingService.Map<Client>(client1Dto)
            };
            return clients;
        }

        public static ICollection<ClientSecret> ClientSecrets() {
            return new List<ClientSecret> {
                MappingService.Map<ClientSecret>(new ClientSecretDto {
                    Key = new Guid("491f98b8-922a-4621-9279-1fa476e30c41"),
                    ClientKey = ClientKey,
                    Value = "123".Sha512()
                })
            };
        }

        public static ICollection<ClientScope> ClientScopes() {
            return new List<ClientScope> {
                MappingService.Map<ClientScope>(new ClientScopeDto {
                    ClientKey = ClientKey,
                    Key = new Guid("be668502-ae52-491f-8616-c120bbf27b80"),
                    Scope = "TestScope"
                }),
                MappingService.Map<ClientScope>(new ClientScopeDto {
                    ClientKey = ClientKey,
                    Key = new Guid("95d74eb4-a9f8-4b8f-a9a3-a10b8307ef59"),
                    Scope = Constants.StandardScopes.Email
                }),
                MappingService.Map<ClientScope>(new ClientScopeDto {
                    ClientKey = ClientKey,
                    Key = new Guid("be668502-ae52-491f-8616-c120bbf27b80"),
                    Scope = Constants.StandardScopes.Phone
                }),
                MappingService.Map<ClientScope>(new ClientScopeDto {
                    ClientKey = ClientKey,
                    Key = new Guid("c2bf9d06-0095-46e5-a1e0-3883aed50e09"),
                    Scope = Constants.StandardScopes.Profile
                }),
                MappingService.Map<ClientScope>(new ClientScopeDto {
                    ClientKey = ClientKey,
                    Key = new Guid("820c4827-6f09-4a62-ac03-d5a13849aac1"),
                    Scope = Constants.StandardScopes.OpenId
                }),
                MappingService.Map<ClientScope>(new ClientScopeDto {
                    ClientKey = ClientKey,
                    Key = new Guid("171e4866-03c6-4bf2-ae04-a54fee16e93d"),
                    Scope = Constants.StandardScopes.OfflineAccess
                })
            };
        }
    }
}