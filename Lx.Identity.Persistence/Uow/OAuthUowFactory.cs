using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lx.Identity.Contracts.Config;
using Lx.Identity.Contracts.DTOs;
using Lx.Identity.Domain.Entities;
using Lx.Identity.Persistence.EF;
using Lx.Utilities.Contract.Caching;
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
        protected static readonly string KeyToScopeHash = typeof(Scope).FullName;
        protected static readonly string KeyToConsentHash = typeof(Consent).FullName;

        protected readonly string ConnectionString;

        public OAuthUowFactory(ILogger logger, IDbConfig config, IMappingService mappingService,
            ISerializer serializer, ICacheFactory cacheFactory, IEventBroadcastingProxy eventBroadcastingProxy)
            : base(config, logger, cacheFactory, mappingService, serializer, eventBroadcastingProxy)
        {
            ConnectionString = config.ConnectionString;
        }

        public void AddOrUpdateClient(ClientDto clientDto)
        {
            Execute(uow => {
                var cacheKey = CacheKeyHelper.GetCacheKey<ClientDto>(clientDto.ClientId);
                uow.Cache.SetCachedItemAsync(cacheKey, clientDto).Wait();
                uow.Store.AddOrUpdate(clientDto, x => x.ClientId == clientDto.ClientId);

                foreach (var allowedScope in clientDto.AllowedScopes)
                    uow.Store.AddOrUpdate(allowedScope,
                        x => (x.Client.ClientId == clientDto.ClientId) && (x.Scope == allowedScope.Scope));

                foreach (var clientSecret in clientDto.ClientSecrets)
                    uow.Store.AddOrUpdate(clientSecret,
                        x => (x.Client.ClientId == clientDto.ClientId) && (x.Value == clientSecret.Value));

                foreach (var redirectUri in clientDto.RedirectUris)
                    uow.Store.AddOrUpdate(redirectUri,
                        x => (x.Client.ClientId == clientDto.ClientId) && (x.Uri == redirectUri.Uri));
            });
        }

        public ClientDto GetClient(string clientId)
        {
            ClientDto clientDto = null;

            Execute(uow => {
                var cacheKey = CacheKeyHelper.GetCacheKey<ClientDto>(clientId);
                clientDto = uow.Cache.GetCachedItem<ClientDto>(cacheKey);
                if (clientDto != null)
                    return;

                clientDto = uow.Store.FirstOrDefault<Client>(x => x.ClientId == clientId,
                    dataSet => dataSet.Include(c => c.AllowedScopes)
                        .Include(c => c.ClientSecrets)
                        .Include(c => c.RedirectUris));
                if (clientDto != null)
                    uow.Cache.SetCachedItemAsync(cacheKey, clientDto).Wait();
            });

            return clientDto;
        }

        public void AddOrUpdateScope(Scope scope)
        {
            Execute(uow => {
                uow.Store.AddOrUpdate(scope, x => x.Name == scope.Name);
                uow.Cache.HashSetAsync(KeyToScopeHash, scope.Name, Serializer.Serialize(scope)).Wait();
            });
        }

        public IReadOnlyCollection<Scope> ListScopes(IEnumerable<string> scopeNames = null)
        {
            var scopes = new List<Scope>();
            var scopeNameList = scopeNames?.ToList();

            Execute(uow => {
                if (scopeNameList == null)
                {
                    scopes.AddRange(uow.Cache.HashGet(KeyToScopeHash)
                        .Select(x => Serializer.Deserialize<Scope>(x.Value)));
                    if (scopes.Any())
                        return;

                    scopes.AddRange(
                        uow.Store.List<Scope>(dbSet => dbSet.Include(s => s.ScopeClaims).Include(s => s.ScopeSecrets)));
                    UpdateScopeHashSet(scopes, uow);
                }
                else
                {
                    scopes.AddRange(
                        uow.Cache.HashGet(KeyToScopeHash, scopeNameList)
                            .Select(x => Serializer.Deserialize<Scope>(x.Value)));
                    if (scopes.Any())
                        return;

                    scopes.AddRange(uow.Store.List<Scope>(
                        dbSet =>
                            dbSet.Include(s => s.ScopeClaims)
                                .Include(s => s.ScopeSecrets)
                                .Where(s => scopeNameList.Contains(s.Name))));
                    UpdateScopeHashSet(scopes, uow);
                }
            });

            return scopes;
        }

        public void AddOrUpdateConsent(Consent consent)
        {
            Execute(uow => {
                uow.Store.AddOrUpdate(consent, x => (x.ClientId == consent.ClientId) && (x.Subject == consent.Subject));

                var consentJson = Serializer.Serialize(consent);
                uow.Cache.HashSetAsync(KeyToConsentHash + consent.Subject, consent.ClientId, consentJson).Wait();
            });
        }

        public IReadOnlyCollection<Consent> ListConsents(string subjectId)
        {
            var consents = new List<Consent>();
            Execute(uow => {
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
            Execute(uow => {
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
            Execute(uow => {
                uow.Cache.HashDeleteAsync(KeyToConsentHash + subjectId, clientId).Wait();
                uow.Store.Delete<Consent>(x => (x.Subject == subjectId) && (x.ClientId == clientId));
            });
        }

        private void UpdateScopeHashSet(IEnumerable<Scope> scopes, IdentityUow uow)
        {
            foreach (var scope in scopes)
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
