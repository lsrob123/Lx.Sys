using System;
using System.Collections.Generic;
using Lx.Utilities.Contract.Authentication.DTOs;
using Lx.Utilities.Contract.Infrastructure.Attributes;
using Lx.Utilities.Contract.Infrastructure.Extensions;
using Lx.Utilities.Contract.Infrastructure.Interfaces;
using Lx.Utilities.Contract.ServiceBus;

namespace Lx.Utilities.Contract.Infrastructure.DTOs {
    public abstract class RequestBase : IRequest, IBusCommand {
        protected RequestBase() {
            ServiceReferences = new List<string>();
        }

        [InvisibleInTestExample]
        public IpAddressSetDto OriginatorIp { get; set; }

        [InvisibleInTestExample]
        public DeviceDto OriginatorDevice { get; set; }

        public string AccessToken { get; set; }

        [InvisibleInTestExample]
        public IdentityDto User { get; set; }

        [InvisibleInTestExample]
        public string OriginatorGroup { get; set; }

        public string RequestReference { get; set; }

        [InvisibleInTestExample]
        public string OriginatorConnection { get; set; }

        [InvisibleInTestExample]
        public ICollection<string> ServiceReferences { get; set; }

        [InvisibleInTestExample]
        public Guid Sid { get; set; }

        public virtual TResponse CreateResponse<TResponse>(ProcessResult processResult = null)
            where TResponse : ResponseBase, new() {
            var response = new TResponse()
                .LinkTo(this)
                .WithProcessResult(processResult);
            return response;
        }

        public TEvent CreateEvent<TEvent>(ProcessResult processResult = null) where TEvent : EventBase, new() {
            var response = new TEvent()
                .LinkTo(this)
                .WithProcessResult(processResult);
            return response;
        }
    }
}