using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Http;
using Lx.Utilities.Contract.Infrastructure.Interfaces;
using Microsoft.Owin;

namespace Lx.Utilities.Services.Web {
    public static class ApiControllerExtensions {
        public const string OwinContextPropertyName = "MS_OwinContext", HttpContextPropertyName = "MS_HttpContext";

        public static HttpResponseMessage ResponseAsHtmlPage(this ApiController controller, string htmlContent,
            Action<HttpResponseMessage> furtherProcess = null) {
            var response = new HttpResponseMessage {
                Content = new StringContent(htmlContent)
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");

            furtherProcess?.Invoke(response);

            return response;
        }

        public static HttpResponseMessage ResponseFromRazorTemplate(this ApiController controller, object viewModel,
            Action<HttpResponseMessage> furtherProcess = null) {
            var razorHtmlGenerationService = new RazorTemplatesService();
            var content = razorHtmlGenerationService.GetHtml(viewModel);
            return ResponseAsHtmlPage(controller, content, furtherProcess);
        }

        public static string GetClientIp(this ApiController controller) {
            return controller.Request.ClientIp();
        }

        public static string ClientIp(this HttpRequestMessage request) {
            if (request.Properties.ContainsKey(HttpContextPropertyName))
                return ((HttpContextWrapper) request
                    .Properties[HttpContextPropertyName]).Request.UserHostAddress;

            if (request.Properties.ContainsKey(OwinContextPropertyName)) {
                var owinContext = (OwinContext) request.Properties[OwinContextPropertyName];
                if (owinContext?.Request?.RemoteIpAddress != null)
                    return owinContext.Request.RemoteIpAddress;
            }

            if (!request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
                return HttpContext.Current == null ? null : HttpContext.Current.Request.UserHostAddress;

            var prop =
                (RemoteEndpointMessageProperty) request.Properties[RemoteEndpointMessageProperty.Name];
            return prop.Address;
        }

        public static void CollectClientIp(this ApiController controller, IHasOriginatorIp request) {
            if (request == null)
                return;

            if (request.OriginatorIp == null)
                request.OriginatorIp = new IpAddressSetDto();

            if (request.OriginatorIp.External == null)
                request.OriginatorIp.External = GetClientIp(controller);
        }

        public static HttpResponseMessage SafeResponse<TResponse>(this ApiController controller,
            TResponse originalResponse, HttpRequestMessage request = null)
            where TResponse : IResponse {
            request = request ?? controller.Request;
            HttpResponseMessage response;

            if (originalResponse.Result?.Type == null) {
                response = request.CreateResponse(HttpStatusCode.OK, originalResponse);
            } else {
                originalResponse.EnsureSecurityForClientSide();
                var statusCode = originalResponse.Result == null
                    ? HttpStatusCode.OK
                    : (HttpStatusCode) originalResponse.Result.Type.Value;

                if ((originalResponse.Result != null) && originalResponse.Result.Type.IsSuccess)
                    response = request.CreateResponse(statusCode,
                        $"{originalResponse.Result.Type.Name} with error reference {originalResponse.Result.ResultReference} encountered.");
                else
                    response = request.CreateResponse(statusCode, originalResponse);
                response.ReasonPhrase = originalResponse.Result?.Reason ?? originalResponse.Result?.Type.Name;
            }

            return response;
        }
    }
}