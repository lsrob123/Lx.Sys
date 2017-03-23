﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lx.Utilities.Contract.Authentication;
using Lx.Utilities.Contract.Authentication.DTOs;
using Lx.Utilities.Contract.Infrastructure.Common;
using Lx.Utilities.Contract.Infrastructure.Dto;
using Lx.Utilities.Contract.Logging;
using Lx.Utilities.Contract.Mapping;
using Lx.Utilities.Services.Infrastructure;
using Lx.Utilities.Services.Reflection;
using Lx.Utilities.Services.Serialization;
using Microsoft.AspNet.SignalR;
using Remotion.Linq.Utilities;
using IRequest = Lx.Utilities.Contract.Infrastructure.Dto.IRequest;

namespace Lx.Utilities.Services.SignalR {
    public abstract class MediatedHubBase : Hub, IMediatorMessageHandler<GeneralDataResponse>, IHasInstanceKey {
        protected static readonly Type MediatorMessageHandlerType = typeof(IMediatorMessageHandler);

        protected readonly ILogger Logger;
        protected readonly IMappingService MappingService;
        protected readonly IOAuthHelper OAuthHelper;

        protected MediatedHubBase(IMediator mediator, ILogger logger, IMappingService mappingService,
            IOAuthHelper oauthHelper = null) {
            InstanceKey = Guid.NewGuid();
            Logger = logger;
            MappingService = mappingService;
            OAuthHelper = oauthHelper;

            if (mediator == null)
                mediator = Mediator.Default;

            mediator.RegisterAllHandlers(this);
        }

        public Guid InstanceKey { get; protected set; }

        public void Handle(GeneralDataResponse message) {
            if (message.HasServiceReference(this))
                SendGroupResponse(message);
        }

        public async Task<string> CreateGroupAsync() {
            return await JoinGroupAsync(Guid.NewGuid().ToString(), true);
        }

        protected virtual string GetGroupName(Guid userKey) {
            return userKey.ToString().ToLower();
        }

        public async Task<string> JoinGroupWithAccessTokenAsync(string accessToken) {
            if ((OAuthHelper == null) || string.IsNullOrWhiteSpace(accessToken))
                return string.Empty;

            var user = await OAuthHelper.GetUserAsync(accessToken);
            if (user == null)
                return string.Empty;

            var groupName = GetGroupName(user.Key);
            await JoinGroupAsync(groupName, true);
            return groupName;
        }

        protected async Task<string> JoinGroupAsync(string groupName, bool enforceNotificationOfAllocatedGroup) {
            var isNewGroupNameRequired = string.IsNullOrWhiteSpace(groupName);
            if (isNewGroupNameRequired)
                groupName = Guid.NewGuid().ToString();

            await Groups.Add(Context.ConnectionId, groupName);

            if (isNewGroupNameRequired || enforceNotificationOfAllocatedGroup)
                Clients.Group(groupName).groupAllocated(groupName);

            return groupName;
        }

        public async Task<string> LeaveGroupAsync(string groupName) {
            if (string.IsNullOrWhiteSpace(groupName))
                groupName = Guid.NewGuid().ToString();

            await Groups.Remove(Context.ConnectionId, groupName);

            return groupName;
        }

        public async Task<IdentityDto> EnsureInGroupAsync(IRequest request, bool tryUseUserKeyAsGroupName = true) {
            this.CollectClientIp(request);

            var enforceNotificationOfAllocatedGroup = false;

            if (tryUseUserKeyAsGroupName && (OAuthHelper != null) && !string.IsNullOrWhiteSpace(request.AccessToken)) {
                var user = await OAuthHelper.GetUserAsync(request.AccessToken);
                if (user != null) {
                    enforceNotificationOfAllocatedGroup =
                        string.IsNullOrWhiteSpace(request.OriginatorGroup) ||
                        !request.OriginatorGroup.Equals(user.UserReference, StringComparison.OrdinalIgnoreCase);
                    request.OriginatorGroup = user.UserReference;
                    request.User = user;
                }
            }

            request.OriginatorGroup = await JoinGroupAsync(request.OriginatorGroup, enforceNotificationOfAllocatedGroup);
            return request.User;
        }

        public async Task GetRequestSampleAsync(string originatorGroup, string methodName) {
            if (string.IsNullOrWhiteSpace(methodName))
                return;

            originatorGroup = await JoinGroupAsync(originatorGroup, false);

            var json = GetRequestSample(methodName);
            if (string.IsNullOrWhiteSpace(json))
                return;

            Clients.Group(originatorGroup).requestSampleReturned(json);
        }

        public string GetRequestSample(string methodName) {
            methodName = methodName.Substring(0, 1).ToUpper() + methodName.Substring(1, methodName.Length - 1);

            var parameterTypes = this.GetMethodParameterTypes(methodName);
            if (!parameterTypes.Any())
                return null;

            var firstParameterInstance = Activator.CreateInstance(parameterTypes.First());
            InitializeComplexTypePropertyValuesRecursively(firstParameterInstance);

            var json = new JsonSerializer().Serialize(firstParameterInstance,
                Casing.Camel);
            return json;
        }

        protected ProcessResult SendGroupResponse<TResponse>(TResponse response, string message = null,
            Action<string, Exception> handleException = null) where TResponse : IResponse {
            var shareGroups = response.ShareGroups();

            var responseToOriginator = MappingService.Map<TResponse>(response);
            var groupResponseToOriginator = new SignalRGroupResponse(responseToOriginator, message);
            var result = this.ExecuteGroupAction(response.OriginatorGroup,
                group => group.groupResponseReceived(groupResponseToOriginator),
                exception => handleException?.Invoke(response.OriginatorGroup, exception));

            if ((shareGroups == null) || !shareGroups.Any())
                return result;

            var responseToShareGroups = MappingService.Map<TResponse>(response);
            var groupResponseToShareGroups = new SignalRGroupResponse(responseToShareGroups);
            foreach (var shareGroup in shareGroups) {
                responseToShareGroups.WithOriginatorGroup(shareGroup);
                this.ExecuteGroupAction(shareGroup,
                    group => group.groupResponseReceived(groupResponseToShareGroups),
                    exception => handleException?.Invoke(shareGroup, exception));
            }

            return result;
        }

        protected void InitializeComplexTypePropertyValuesRecursively(object instance) {
            var stringType = typeof(string);
            var dateTimeOffsetType = typeof(DateTimeOffset);
            var timeSpanType = typeof(TimeSpan);
            var genericNumerableType = typeof(IEnumerable);
            var userType = typeof(IdentityDto);

            var properties = instance.GetType().GetProperties();
            foreach (var property in properties)
                try {
                    if ((property.PropertyType == userType) ||
                        property.Name.Equals(nameof(IRequestKey.ServiceReferences)))
                        continue;

                    if (stringType.IsAssignableFrom(property.PropertyType)) {
                        property.SetValue(instance, string.Empty);
                        continue;
                    }

                    if (dateTimeOffsetType.IsAssignableFrom(property.PropertyType)) {
                        property.SetValue(instance, DateTimeOffset.UtcNow);
                        continue;
                    }

                    if (timeSpanType.IsAssignableFrom(property.PropertyType)) {
                        property.SetValue(instance, TimeSpan.FromMinutes(1));
                        continue;
                    }

                    if (genericNumerableType.IsAssignableFrom(property.PropertyType)) {
                        var genericArguments = property.PropertyType.GetGenericArguments();
                        if (!genericArguments.Any())
                            throw new ArgumentEmptyException(nameof(property.Name));

                        var elementType = genericArguments.First();
                        var listType = typeof(List<>).MakeGenericType(elementType);
                        var list = Activator.CreateInstance(listType) as IList;

                        if (list != null) {
                            var element = Activator.CreateInstance(elementType);
                            InitializeComplexTypePropertyValuesRecursively(element);
                            list.Add(element);

                            element = Activator.CreateInstance(elementType);
                            InitializeComplexTypePropertyValuesRecursively(element);
                            list.Add(element);

                            property.SetValue(instance, list);
                        }

                        continue;
                    }

                    if (property.PropertyType.IsPrimitive)
                        continue;

                    var propertyInstance = Activator.CreateInstance(property.PropertyType);
                    InitializeComplexTypePropertyValuesRecursively(propertyInstance);

                    property.SetValue(instance, propertyInstance);
                } catch (Exception ex) {
                    new Exception(property.Name + ":" + property.PropertyType.FullName, ex).WriteToLog(Logger);
                }
        }

        public override Task OnConnected() {
            var connId = Context.ConnectionId;
            Console.WriteLine("OnConnected " + connId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled) {
            var connId = Context.ConnectionId;
            Console.WriteLine("OnDisconnected " + connId);
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected() {
            var connId = Context.ConnectionId;
            Console.WriteLine("OnReconnected " + connId);
            return base.OnReconnected();
        }
    }
}