using Lx.Utilities.Contract.Infrastructure.Dto;

namespace Lx.Utilities.Services.SignalR {
    public class SignalRGroupResponse {
        public SignalRGroupResponse() {}

        public SignalRGroupResponse(object data, string message = null) {
            (data as IResponse)?.EnsureSecurityForClientSide();

            Data = data;
            DataType = data.GetType().Name;
            Message = message;
        }

        public string DataType { get; protected set; }

        public object Data { get; protected set; }

        public string Message { get; protected set; }
    }
}