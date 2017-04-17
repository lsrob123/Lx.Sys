﻿using System.Collections.Generic;
using Lx.Identity.Contracts.DTOs;
using Lx.Identity.Persistence.Uow;
using Lx.Utilities.Contract.Mapping;

namespace Lx.Identity.Services.Services
{
    public class ScopeService : IScopeService
    {
        protected readonly IMappingService MappingService;
        protected readonly IOAuthUowFactory UowFactory;

        public ScopeService(IMappingService mappingService, IOAuthUowFactory uowFactory)
        {
            MappingService = mappingService;
            UowFactory = uowFactory;
        }

        public ICollection<ScopeDto> ListScopes(IEnumerable<string> scopeNames)
        {
            var scopes = UowFactory.ListScopes(scopeNames);
            return scopes;
        }
    }
}