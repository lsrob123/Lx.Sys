using System;
using System.Collections.Generic;
using Lx.Utilities.Contract.Authentication.DTOs;
using Lx.Utilities.Contract.Infrastructure.Interfaces;
using Lx.Utilities.Contract.ServiceBus;

namespace Lx.Utilities.Contract.Infrastructure.DTOs {
    public abstract class RequestBase : IRequest, IBusCommand {
        protected RequestBase() {
            ServiceReferences = new List<string>();
        }

        public IpAddressSetDto OriginatorIp { get; set; }
        public DeviceDto OriginatorDevice { get; set; }
        public string AccessToken { get; set; }
        public IdentityDto User { get; set; }
        public string OriginatorGroup { get; set; }
        public string RequestReference { get; set; }
        public string OriginatorConnection { get; set; }
        public ICollection<string> ServiceReferences { get; set; }
        public Guid Sid { get; set; }

        public virtual TResponse CreateResponse<TResponse>(ProcessResult processResult = null)
            where TResponse : ResponseBase, new() {
            var response = new TResponse()
                .LinkTo(this)
                .WithProcessResult(processResult);
            return response;
        }
    }
}