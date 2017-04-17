using System.Collections.Generic;
using System.Linq;
using Lx.Identity.Contracts.DTOs;
using Lx.Identity.Domain.Entities;
using Lx.Identity.Persistence.EF;
using Lx.Utilities.Contract.Caching;
using Lx.Utilities.Contract.Infrastructure.Domain;
using Lx.Utilities.Contract.Infrastructure.EventBroadcasting;
using Lx.Utilities.Contract.Logging;
using Lx.Utilities.Contract.Mapping;
using Lx.Utilities.Contract.Persistence;
using Lx.Utilities.Contract.Serialization;
using Lx.Utilities.Services.Persistence;

namespace Lx.Identity.Persistence.Uow
{
    public class OAuthUowFactory : UnitOfWorkFactoryBase<IdentityUow>, IOAuthUowFactory
    {
        protected static readonly string KeyToScopeHash = typeof (Scope).FullName;
        protected static readonly string KeyToConsentHash = typeof (Consent).FullName;
        protected readonly string ConnectionString;

        public OAuthUowFactory(ILogger logger, IDbConfig config, IMappingService mappingService,
            ISerializer serializer, ICacheFactory cacheFactory, IEventBroadcastingProxy eventBroadcastingProxy)
            : base(config, logger, cacheFactory, mappingService, serializer, eventBroadcastingProxy)
        {
            ConnectionString = config.ConnectionString;
        }

        public ClientDto AddOrUpdateClient(ClientDto clientDto)
        {
            ClientDto updatedClientDto = null;
            Execute(uow => updatedClientDto = AddOrUpdateClient(uow, clientDto));

            return updatedClientDto;
        }

        public ClientDto GetClient(string clientId)
        {
            ClientDto clientDto = null;
            Execute(uow => clientDto = GetClient(uow, clientId));

            return clientDto;
        }

        public ScopeDto AddOrUpdateScope(ScopeDto scopeDto)
        {
            ScopeDto updatedScopeDto = null;
            Execute(uow =>
            {
                var scope = uow.Store.AddOrUpdate(MappingService.Map<Scope>(scopeDto).WithValidKey(),
                    x => x.Name == scopeDto.Name);
                updatedScopeDto = MappingService.Map<ScopeDto>(scope);

                if (scopeDto.ScopeClaims != null)
                    foreach (var scopeClaim in
                        scopeDto.ScopeClaims.Select(
                            scopeClaimDto => MappingService.Map<ScopeClaim>(scopeClaimDto).WithScope(scope)))
                    {
                        uow.Store.AddOrUpdate(scopeClaim, x => x.Name == scopeClaim.Name);
                    }

                if (scopeDto.ScopeSecrets != null)
                    foreach (var scopeSecret in
                        scopeDto.ScopeSecrets.Select(
                            scopeSecretDto => MappingService.Map<ScopeSecret>(scopeSecretDto).WithScope(scope)))
                    {
                        uow.Store.AddOrUpdate(scopeSecret, x => x.Type == scopeSecret.Type);
                    }

                CollectScopeAssociatedInfo(uow, updatedScopeDto);
                uow.Cache.HashSetAsync(KeyToScopeHash, updatedScopeDto.Name, Serializer.Serialize(updatedScopeDto))
                    .Wait();
            });
            return updatedScopeDto;
        }

        public ICollection<ScopeDto> ListScopes(IEnumerable<string> scopeNames = null)
        {
            var scopeDtos = new List<ScopeDto>();
            Execute(uow => ListScopes(uow, scopeNames, scopeDtos));

            return scopeDtos;
        }

        public void AddOrUpdateConsent(Consent consent)
        {
            Execute(uow =>
            {
                uow.Store.AddOrUpdate(consent, x => (x.ClientId == consent.ClientId) && (x.Subject == consent.Subject));
                var consentJson = Serializer.Serialize(consent);
                uow.Cache.HashSetAsync(KeyToConsentHash + consent.Subject, consent.ClientId, consentJson).Wait();
            });
        }

        public ICollection<Consent> ListConsents(string subjectId)
        {
            var consents = new List<Consent>();
            Execute(uow =>
            {
                consents.AddRange(uow.Cache.HashGet(KeyToConsentHash + subjectId).Values
                    .Select(x => Serializer.Deserialize<Consent>(x)));
                if (!consents.Any())
                    consents.AddRange(uow.Store.List<Consent>(x => x.Where(y => y.Subject == subjectId)));
            });

            return consents;
        }

        public Consent GetConsent(string subjectId, string clientId)
        {
            Consent consent = null;
            Execute(uow =>
            {
                var consentJson = uow.Cache.HashGet(KeyToConsentHash + subjectId, clientId);
                if (consentJson != null)
                {
                    consent = Serializer.Deserialize<Consent>(consentJson);
                    return;
                }

                consent = uow.Store.FirstOrDefault<Consent>(x => (x.Subject == subjectId) && (x.ClientId == clientId));
            });

            return consent;
        }

        public void RemoveConsent(string subjectId, string clientId)
        {
            Execute(uow =>
            {
                uow.Cache.HashDeleteAsync(KeyToConsentHash + subjectId, clientId).Wait();
                uow.Store.Delete<Consent>(x => (x.Subject == subjectId) && (x.ClientId == clientId));
            });
        }

        private ClientDto AddOrUpdateClient(IdentityUow uow, ClientDto clientDto)
        {
            var client = MappingService.Map<Client>(clientDto).WithValidKey();

            client = uow.Store.AddOrUpdate(client, x => x.ClientId == clientDto.ClientId);

            if (clientDto.AllowedScopes != null)
                foreach (var allowedScopeDto in clientDto.AllowedScopes)
                {
                    var allowedScope = MappingService.Map<ClientScope>(allowedScopeDto).WithClient(client);
                    uow.Store.AddOrUpdate(allowedScope,
                        x => (x.ClientKey == client.Key) && (x.Scope == allowedScopeDto.Scope));
                }

            if (clientDto.ClientSecrets != null)
                foreach (var clientSecretDto in clientDto.ClientSecrets)
                {
                    var clientSecret = MappingService.Map<ClientSecret>(clientSecretDto).WithClient(client);
                    uow.Store.AddOrUpdate(clientSecret,
                        x => (x.ClientKey == client.Key) && (x.Value == clientSecretDto.Value));
                }

            if (clientDto.RedirectUris != null)
                foreach (var redirectUriDto in clientDto.RedirectUris)
                {
                    var redirectUri = MappingService.Map<ClientRedirectUri>(redirectUriDto).WithClient(client);
                    uow.Store.AddOrUpdate(redirectUri,
                        x => (x.ClientKey == client.Key) && (x.Uri == redirectUriDto.Uri));
                }

            var updatedClientDto = MappingService.Map<ClientDto>(client);
            CollectClientAssociatedInfo(uow, updatedClientDto);

            var cacheKey = CacheKeyHelper.GetCacheKey<ClientDto>(client.ClientId);
            uow.Cache.SetCachedItemAsync(cacheKey, updatedClientDto).Wait();
            return updatedClientDto;
        }

        private void CollectClientAssociatedInfo(IdentityUow uow, ClientDto clientDto)
        {
            clientDto.AllowedScopes = uow.Store
                .List<ClientScope>(set => set.Where(x => x.ClientKey == clientDto.Key))
                .Select(x => MappingService.Map<ClientScopeDto>(x))
                .ToList();
            clientDto.ClientSecrets = uow.Store
                .List<ClientSecret>(set => set.Where(x => x.ClientKey == clientDto.Key))
                .Select(x => MappingService.Map<ClientSecretDto>(x))
                .ToList();
            clientDto.RedirectUris = uow.Store
                .List<ClientRedirectUri>(set => set.Where(x => x.ClientKey == clientDto.Key))
                .Select(x => MappingService.Map<ClientRedirectUriDto>(x))
                .ToList();
        }

        private ClientDto GetClient(IdentityUow uow, string clientId)
        {
            var cacheKey = CacheKeyHelper.GetCacheKey<ClientDto>(clientId);
            var clientDto = uow.Cache.GetCachedItem<ClientDto>(cacheKey);
            if (clientDto != null)
                return clientDto;

            clientDto = MappingService.Map<ClientDto>(uow.Store.FirstOrDefault<Client>(x => x.ClientId == clientId));
            if (clientDto == null)
                return null;

            CollectClientAssociatedInfo(uow, clientDto);
            uow.Cache.SetCachedItemAsync(cacheKey, clientDto).Wait();
            return clientDto;
        }

        private void CollectScopeAssociatedInfo(IdentityUow uow, ScopeDto scopeDto)
        {
            scopeDto.ScopeClaims = uow.Store.List<ScopeClaim>(set => set.Where(x => x.ScopeKey == scopeDto.Key))
                .Select(x => MappingService.Map<ScopeClaimDto>(x))
                .ToList();
            scopeDto.ScopeSecrets = uow.Store.List<ScopeSecret>(set => set.Where(x => x.ScopeKey == scopeDto.Key))
                .Select(x => MappingService.Map<ScopeSecretDto>(x))
                .ToList();
        }

        private void ListScopes(IdentityUow uow, IEnumerable<string> scopeNames, List<ScopeDto> scopeDtos)
        {
            scopeDtos.AddRange(uow.Cache.HashGet(KeyToScopeHash)
                .Select(x => Serializer.Deserialize<ScopeDto>(x.Value)));
            if (scopeDtos.Any())
                return;

            var scopeNameList = scopeNames?.ToList();
            var scopes = uow.Store.DbSetAsQueryable<Scope>().ToList();
            var selectedScopeDtos = scopes.Select(scope => MappingService.Map<ScopeDto>(scope))
                .Where(scopeDto => scopeNameList == null || scopeNameList.Contains(scopeDto.Name))
                .ToList();

            foreach (var scopeDto in selectedScopeDtos)
            {
                CollectScopeAssociatedInfo(uow, scopeDto);
                scopeDtos.Add(scopeDto);
            }

            UpdateScopeHashSet(uow, scopeDtos);
        }

        private void UpdateScopeHashSet(IdentityUow uow, IEnumerable<ScopeDto> scopeDtos)
        {
            foreach (var scope in scopeDtos)
                uow.Cache.HashSetAsync(KeyToScopeHash, scope.Name, Serializer.Serialize(scope)).Wait();
        }

        protected override IdentityUow GetUnitOfWork()
        {
            var unitOfWork = new IdentityUow(() => new IdentityDbContext(ConnectionString), CacheFactory,
                MappingService, Logger);
            return unitOfWork;
        }
    }
}