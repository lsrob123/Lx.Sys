using System;
using Lx.Utilities.Contract.Infrastructure.Interfaces;
using Lx.Utilities.Contract.Mapping;

namespace Lx.Utilities.Contract.Infrastructure.DTOs {
    public static class RepublishedResponseExtensions {
        public static TRepublish With<TRepublish, TOriginalResponse>(this TRepublish republish,
            Func<TOriginalResponse, TOriginalResponse> replicateResponseInstance,
            string originatorGroup,
            TOriginalResponse originalResponse)
            where TOriginalResponse : class, IResponse
            where TRepublish : RepublishedResponseBase<TOriginalResponse> {
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
            where TRepublish : RepublishedResponseBase<TOriginalResponse> {
            return republish.With(mappingService.Map<TOriginalResponse>, originatorGroup, originalResponse);
        }
    }
}