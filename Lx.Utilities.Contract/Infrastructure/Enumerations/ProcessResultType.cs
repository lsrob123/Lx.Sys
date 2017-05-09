using Lx.Utilities.Contract.Infrastructure.Common;

namespace Lx.Utilities.Contract.Infrastructure.Enumerations {
    public class ProcessResultType : Enumeration {
        public static ProcessResultType Unknown = new ProcessResultType(0, nameof(Unknown));
        public static ProcessResultType Continue = new ProcessResultType(100, nameof(Continue));
        public static ProcessResultType SwitchingProtocols = new ProcessResultType(101, nameof(SwitchingProtocols));
        public static ProcessResultType Ok = new ProcessResultType(200, nameof(Ok).ToUpper(), true);
        public static ProcessResultType Created = new ProcessResultType(201, nameof(Created), true);
        public static ProcessResultType Accepted = new ProcessResultType(202, nameof(Accepted), true);

        public static ProcessResultType NonAuthoritativeInformation = new ProcessResultType(203,
            nameof(NonAuthoritativeInformation));

        public static ProcessResultType NoContent = new ProcessResultType(204, nameof(NoContent));
        public static ProcessResultType ResetContent = new ProcessResultType(205, nameof(ResetContent));
        public static ProcessResultType PartialContent = new ProcessResultType(206, nameof(PartialContent));
        public static ProcessResultType MultiStatus = new ProcessResultType(207, nameof(MultiStatus));
        public static ProcessResultType MultipleChoices = new ProcessResultType(300, nameof(MultipleChoices));
        public static ProcessResultType Ambiguous = new ProcessResultType(300, nameof(Ambiguous));
        public static ProcessResultType MovedPermanently = new ProcessResultType(301, nameof(MovedPermanently));
        public static ProcessResultType Moved = new ProcessResultType(301, nameof(Moved));
        public static ProcessResultType Found = new ProcessResultType(302, nameof(Found));
        public static ProcessResultType Redirect = new ProcessResultType(302, nameof(Redirect));
        public static ProcessResultType SeeOther = new ProcessResultType(303, nameof(SeeOther));
        public static ProcessResultType RedirectMethod = new ProcessResultType(303, nameof(RedirectMethod));
        public static ProcessResultType NotModified = new ProcessResultType(304, nameof(NotModified));
        public static ProcessResultType UseProxy = new ProcessResultType(305, nameof(UseProxy));
        public static ProcessResultType Unused = new ProcessResultType(306, nameof(Unused));
        public static ProcessResultType TemporaryRedirect = new ProcessResultType(307, nameof(TemporaryRedirect));
        public static ProcessResultType RedirectKeepVerb = new ProcessResultType(307, nameof(RedirectKeepVerb));
        public static ProcessResultType BadRequest = new ProcessResultType(400, nameof(BadRequest));
        public static ProcessResultType Unauthorized = new ProcessResultType(401, nameof(Unauthorized));
        public static ProcessResultType PaymentRequired = new ProcessResultType(402, nameof(PaymentRequired));
        public static ProcessResultType Forbidden = new ProcessResultType(403, nameof(Forbidden));
        public static ProcessResultType NotFound = new ProcessResultType(404, nameof(NotFound));
        public static ProcessResultType MethodNotAllowed = new ProcessResultType(405, nameof(MethodNotAllowed));
        public static ProcessResultType NotAcceptable = new ProcessResultType(406, nameof(NotAcceptable));

        public static ProcessResultType ProxyAuthenticationRequired = new ProcessResultType(407,
            nameof(ProxyAuthenticationRequired));

        public static ProcessResultType RequestTimeout = new ProcessResultType(408, nameof(RequestTimeout));
        public static ProcessResultType Conflict = new ProcessResultType(409, nameof(Conflict));
        public static ProcessResultType Gone = new ProcessResultType(410, nameof(Gone));
        public static ProcessResultType LengthRequired = new ProcessResultType(411, nameof(LengthRequired));
        public static ProcessResultType PreconditionFailed = new ProcessResultType(412, nameof(PreconditionFailed));

        public static ProcessResultType RequestEntityTooLarge =
            new ProcessResultType(413, nameof(RequestEntityTooLarge));

        public static ProcessResultType RequestUriTooLong = new ProcessResultType(414, nameof(RequestUriTooLong));
        public static ProcessResultType UnsupportedMediaType = new ProcessResultType(415, nameof(UnsupportedMediaType));

        public static ProcessResultType RequestedRangeNotSatisfiable = new ProcessResultType(416,
            nameof(RequestedRangeNotSatisfiable));

        public static ProcessResultType ExpectationFailed = new ProcessResultType(417, nameof(ExpectationFailed));
        public static ProcessResultType UpgradeRequired = new ProcessResultType(426, nameof(UpgradeRequired));
        public static ProcessResultType InternalServerError = new ProcessResultType(500, nameof(InternalServerError));
        public static ProcessResultType NotImplemented = new ProcessResultType(501, nameof(NotImplemented));
        public static ProcessResultType BadGateway = new ProcessResultType(502, nameof(BadGateway));
        public static ProcessResultType ServiceUnavailable = new ProcessResultType(503, nameof(ServiceUnavailable));
        public static ProcessResultType GatewayTimeout = new ProcessResultType(504, nameof(GatewayTimeout));

        public static ProcessResultType HttpVersionNotSupported = new ProcessResultType(505,
            nameof(HttpVersionNotSupported));

        public ProcessResultType() { }

        public ProcessResultType(int value, string name, bool isSuccess = false)
            : base(value, name) {
            IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; set; }
    }
}