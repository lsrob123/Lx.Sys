using System;
using System.Collections.Generic;
using Lx.Utilities.Contracts.Infrastructure.DTOs;
using Lx.Utilities.Contracts.Infrastructure.Enumerations;
using Lx.Utilities.Contracts.Infrastructure.Interfaces;
using Microsoft.AspNet.SignalR;
using IRequest = Microsoft.AspNet.SignalR.IRequest;

namespace Lx.Utilities.Services.SignalR
{
    public static class HubExtensions
    {
        private const string RemoteIpAddressToHub = "server.RemoteIpAddress";

        public static ProcessResult ExecuteGroupAction(this Hub hub, string groupName, Action<dynamic> action,
            Action<Exception> handleException = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(groupName))
                    throw new ArgumentNullException(nameof(groupName));

                var group = hub.Clients.Group(groupName);
                if (group == null)
                    throw new KeyNotFoundException($"SignalR group {nameof(groupName)} is not found.");

                action?.Invoke(group);

                return ProcessResultType.Ok;
            }
            catch (Exception exception)
            {
                handleException?.Invoke(exception);
                return new ProcessResult(exception, logExcetions: !(exception is NullReferenceException));
            }
        }

        public static string GetClientIp(this Hub hub)
        {
            return hub.Context.Request.ClientIp();
        }

        public static string ClientIp(this IRequest request)
        {
            string ipAddress = null;
            object tempObject;

            request.Environment.TryGetValue(RemoteIpAddressToHub, out tempObject);

            if (tempObject != null)
                ipAddress = (string) tempObject;

            return ipAddress;
        }

        public static Hub CollectClientIp(this Hub hub, IHasOriginatorIp request)
        {
            if (request == null)
                return hub;

            if (request.OriginatorIp == null)
                request.OriginatorIp = new IpAddressSetDto();

            if (request.OriginatorIp.External == null)
                request.OriginatorIp.External = GetClientIp(hub);

            return hub;
        }

        public static THub ExecuteOnAllClients<THub, TResponse>(this THub hub, TResponse response,
            Action<dynamic, SignalRGroupResponse> actionsOnAllClients)
            where THub : Hub
            where TResponse : IResponse
        {
            var groupResponse = new SignalRGroupResponse(response);
            actionsOnAllClients?.Invoke(hub.Clients.All, groupResponse);
            return hub;
        }
    }
}