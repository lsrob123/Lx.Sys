using System;
using Lx.Utilities.Contracts.Infrastructure.DTOs;
using Lx.Utilities.Contracts.Infrastructure.Interfaces;
using Lx.Utilities.Contracts.Mapping;

namespace Lx.Utilities.Contracts.Infrastructure.Extensions
{
    public static class RepublishedResponseExtensions
    {
        public static TRepublish With<TRepublish, TOriginalResponse>(this TRepublish republish,
            Func<TOriginalResponse, TOriginalResponse> replicateResponseInstance,
            string originatorGroup,
            TOriginalResponse originalResponse)
            where TOriginalResponse : class, IResponse
            where TRepublish : RepublishedResponseBase<TOriginalResponse>
        {
            var response = replicateResponseInstance?.Invoke(originalResponse) ?? originalResponse;
            republish.OriginalResponse = response;
            republish.OriginatorGroup = originatorGroup;
            return republish;
        }

        public static TRepublish With<TRepublish, TOriginalResponse>(this TRepublish republish,
            IMappingService mappingService,
            string originatorGroup,
            TOriginalResponse originalResponse)
            where TOriginalResponse : class, IResponse
            where TRepublish : RepublishedResponseBase<TOriginalResponse>
        {
            return republish.With(x => mappingService.Map<TOriginalResponse>(x), originatorGroup, originalResponse);
        }
    }
}