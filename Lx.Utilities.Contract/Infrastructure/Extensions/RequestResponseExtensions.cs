using System;
using System.Collections.Generic;
using System.Linq;
using Lx.Utilities.Contract.Authentication.DTOs;
using Lx.Utilities.Contract.Infrastructure.DTOs;
using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Utilities.Contract.Infrastructure.Extensions
{
    public static class RequestResponseExtensions
    {
        public static TRequest WithUser<TRequest>(this TRequest result, IdentityDto user)
            where TRequest : IRequest
        {
            result.User = user;
            result.OriginatorGroup = user.Key.ToString();
            return result;
        }

        public static TResult WithProcessResult<TResult>(this TResult result, ProcessResult processResult,
            string reason = null)
            where TResult : ResultBase
        {
            result.Result = processResult.WithReason(reason);
            return result;
        }

        public static TResult WithReason<TResult>(this TResult result, string reason)
            where TResult : ResultBase
        {
            result.Result?.WithReason(reason);
            return result;
        }

        /// <summary>
        ///     Link to another request/response object with same OriginatorGroup, RequestReference
        ///     and possibly Sid (saga ID)
        /// </summary>
        /// <typeparam name="TRequestKey"></typeparam>
        /// <param name="basicRequestKey"></param>
        /// <param name="other"></param>
        /// <param name="alternativeGroup"></param>
        /// <returns></returns>
        public static TRequestKey LinkTo<TRequestKey>(this TRequestKey basicRequestKey, IBasicRequestKey other,
            string alternativeGroup = null)
            where TRequestKey : IBasicRequestKey
        {
            if (other == null)
                return basicRequestKey;

            basicRequestKey.OriginatorGroup = string.IsNullOrWhiteSpace(alternativeGroup)
                ? other.OriginatorGroup
                : alternativeGroup.Trim();
            basicRequestKey.RequestReference = other.RequestReference;
            basicRequestKey.OriginatorConnection = other.OriginatorConnection;

            var requestKey = basicRequestKey as IRequestKey;
            if (requestKey != null)
            {
                var otherAsRequestKey = other as IRequestKey;
                if (otherAsRequestKey != null)
                    requestKey.ServiceReferences = otherAsRequestKey.ServiceReferences;
            }

            var withSagaId = basicRequestKey as IHasSagaId;
            if (withSagaId == null)
                return basicRequestKey;

            var otherWithSagaId = other as IHasSagaId;
            if (otherWithSagaId != null)
                withSagaId.Sid = otherWithSagaId.Sid;

            return basicRequestKey;
        }

        /// <summary>
        ///     Add a service reference to the request or response object
        /// </summary>
        /// <typeparam name="TRequestKey"></typeparam>
        /// <param name="requestKey"></param>
        /// <param name="serviceReference"></param>
        /// <returns></returns>
        public static TRequestKey AddServiceReference<TRequestKey>(this TRequestKey requestKey, string serviceReference)
            where TRequestKey : IRequestKey
        {
            if (requestKey.ServiceReferences == null)
                requestKey.ServiceReferences = new List<string>();

            requestKey.ServiceReferences.Add(serviceReference);
            return requestKey;
        }

        /// <summary>
        ///     Add a service reference to the request or response object
        /// </summary>
        /// <typeparam name="TRequestKey"></typeparam>
        /// <param name="requestKey"></param>
        /// <param name="service"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static TRequestKey AddServiceReference<TRequestKey>(this TRequestKey requestKey, IHasInstanceKey service,
            string suffix = null)
            where TRequestKey : IRequestKey
        {
            requestKey.AddServiceReference(CreateServiceReference(service, suffix));
            return requestKey;
        }

        private static string CreateServiceReference(IHasInstanceKey service, string suffix)
        {
            return service.InstanceKey + (suffix ?? string.Empty);
        }

        /// <summary>
        ///     Check is a service has been referenced in the request or response object
        /// </summary>
        /// <typeparam name="TRequestKey"></typeparam>
        /// <param name="requestKey"></param>
        /// <param name="serviceReference"></param>
        /// <returns></returns>
        public static bool HasServiceReference<TRequestKey>(this TRequestKey requestKey, string serviceReference)
            where TRequestKey : IRequestKey
        {
            var hasServiceReference =
                requestKey.ServiceReferences != null && requestKey.ServiceReferences.Any(
                    x => x.Equals(serviceReference, StringComparison.OrdinalIgnoreCase));

            return hasServiceReference;
        }

        public static bool HasServiceReference<TRequestKey>(this TRequestKey requestKey, IHasInstanceKey service,
            string suffix = null)
            where TRequestKey : IRequestKey
        {
            return requestKey.HasServiceReference(CreateServiceReference(service, suffix));
        }

        public static TRequestKey WithOriginatorGroup<TRequestKey>(this TRequestKey requestKey, string groupName)
            where TRequestKey : IRequestKey
        {
            requestKey.OriginatorGroup = groupName;
            return requestKey;
        }
    }
}