using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Lx.Utilities.Contract.Graphics;
using Lx.Utilities.Contract.Infrastructure.Enumerations;
using Lx.Utilities.Contract.Infrastructure.Extensions;
using Lx.Utilities.Contract.Infrastructure.Interfaces;
using Lx.Utilities.Contract.Web;
using Microsoft.Owin;

namespace Lx.Utilities.Services.Web
{
    public static class ApiControllerExtensions
    {
        private const string OwinContextPropertyName = "MS_OwinContext",
            HttpContextPropertyName = "MS_HttpContext",
            SearchPatternForTempUploadFiles = "BodyPart_*",
            MediaTypeImageJpeg = "image/jpeg",
            MediaTypeTextHtml = "text/html",
            AcceptRangeBytes = "bytes";

        public static HttpResponseMessage ResponseAsHtmlPage(this ApiController controller, string htmlContent,
            Action<HttpResponseMessage> furtherProcess = null)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(htmlContent)
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeTextHtml);

            furtherProcess?.Invoke(response);

            return response;
        }

        public static HttpResponseMessage ResponseFromRazorTemplate(this ApiController controller, object viewModel,
            Action<HttpResponseMessage> furtherProcess = null)
        {
            var razorHtmlGenerationService = new RazorTemplatesService();
            var content = razorHtmlGenerationService.GetHtml(viewModel);
            return ResponseAsHtmlPage(controller, content, furtherProcess);
        }

        public static string GetClientIp(this ApiController controller)
        {
            return controller.Request.ClientIp();
        }

        public static string ClientIp(this HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey(HttpContextPropertyName))
                return ((HttpContextWrapper) request
                    .Properties[HttpContextPropertyName]).Request.UserHostAddress;

            if (request.Properties.ContainsKey(OwinContextPropertyName))
            {
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

        public static void CollectClientIp(this ApiController controller, IHasOriginatorIp request)
        {
            if (request == null)
                return;

            if (request.OriginatorIp == null)
                request.OriginatorIp = new IpAddressSetDto();

            if (request.OriginatorIp.External == null)
                request.OriginatorIp.External = GetClientIp(controller);
        }

        public static HttpResponseMessage SafeResponse<TResponse>(this ApiController controller,
            TResponse originalResponse, HttpRequestMessage request = null)
            where TResponse : IResponse
        {
            request = request ?? controller.Request;
            HttpResponseMessage response;

            if (originalResponse.Result?.Type == null)
            {
                response = request.CreateResponse(HttpStatusCode.OK, originalResponse);
            }
            else
            {
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

        public static async Task<UploadFileResult> ProcessUploadFilesAsync(this ApiController controller,
            string destinationFolder, Func<string, string> getDestinationFileName = null,
            Func<string, string> getEnforcedFileNameExtension = null, IImageProcessor imageProcessor = null)
        {
            var filesToBeDeleted = Directory.GetFiles(destinationFolder, SearchPatternForTempUploadFiles);
            foreach (var fileToBeDeleted in filesToBeDeleted)
                File.Delete(fileToBeDeleted);

            var request = controller.Request;
            if (!request.Content.IsMimeMultipartContent())
                return new UploadFileResult()
                    .WithProcessResult(ProcessResultType.UnsupportedMediaType, "Media type is not supported");

            if (!Directory.Exists(destinationFolder))
                Directory.CreateDirectory(destinationFolder);

            var provider = new MultipartFormDataStreamProvider(destinationFolder);
            await request.Content.ReadAsMultipartAsync(provider);

            var result = new UploadFileResult
            {
                UploadFiles = provider.FileData
                    .Select(x => new UploadFileInfoDto
                    {
                        MediaType = x.Headers.ContentType.MediaType,
                        FileName = x.Headers.ContentDisposition.FileName.Replace("\"", string.Empty),
                        TempFileName = x.LocalFileName
                    }).ToList(),
                TimeUploaded = DateTimeOffset.UtcNow
            };

            var exceptions = new List<Exception>();
            foreach (var uploadFileInfo in result.UploadFiles)
                try
                {
                    var tempFile = Path.Combine(destinationFolder, uploadFileInfo.TempFileName);

                    var processedFile = tempFile;
                    if (imageProcessor != null)
                    {
                        processedFile = Path.Combine(destinationFolder, Guid.NewGuid().ToString());
                        imageProcessor.Process(tempFile, processedFile);
                    }

                    var fileName = getDestinationFileName?.Invoke(uploadFileInfo.FileName) ?? uploadFileInfo.FileName;
                    if (getEnforcedFileNameExtension == null)
                        uploadFileInfo.FullFilePath = Path.Combine(destinationFolder, fileName);
                    else
                        uploadFileInfo.FullFilePath = Path.Combine(destinationFolder,
                            Path.GetFileNameWithoutExtension(fileName) + '.' +
                            getEnforcedFileNameExtension(fileName).TrimStart('.'));

                    if (File.Exists(uploadFileInfo.FullFilePath))
                        File.Delete(uploadFileInfo.FullFilePath);

                    File.Move(processedFile, uploadFileInfo.FullFilePath);
                    File.Delete(processedFile);
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }

            result = exceptions.Any()
                ? result.WithProcessResult(exceptions)
                : result.WithProcessResult(ProcessResultType.Ok);
            return result;
        }

        public static async Task<HttpResponseMessage> CreateImageResponseAsync(this ApiController controller,
            string fileIdntifier, string filePathPhysical)
        {
            var request = controller.Request;

            if (!File.Exists(filePathPhysical))
                return request.CreateErrorResponse(HttpStatusCode.NotFound, "Image is not found.");

            var timeLastWrite = File.GetLastWriteTimeUtc(filePathPhysical);
            var timeLastWriteOffset = new DateTimeOffset(timeLastWrite);
            var timeLastWriteString = timeLastWrite.ToFileTimeUtc().ToString();
            var eTag = new EntityTagHeaderValue(fileIdntifier + timeLastWriteString);

            var ifModifiedSince = request.Headers.IfModifiedSince;
            if (ifModifiedSince.HasValue && (timeLastWriteOffset > ifModifiedSince.Value))
                return CreateNotModifiedResponse(request, eTag, timeLastWriteOffset);

            if (request.Headers.IfNoneMatch.Any(x => x.Equals(eTag)))
                return CreateNotModifiedResponse(request, eTag, timeLastWriteOffset);

            var content = await Task.Run(() => new FileStream(filePathPhysical, FileMode.Open))
                .ContinueWith(t => Image.FromStream(t.Result))
                .ContinueWith(t =>
                {
                    var x = new MemoryStream();
                    t.Result.Save(x, ImageFormat.Jpeg);
                    var y = new ByteArrayContent(x.ToArray());
                    y.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeImageJpeg);
                    return y;
                });

            var response = request.CreateResponse(content);
            SetCaching(response, eTag, timeLastWriteOffset);
            return response;
        }

        private static HttpResponseMessage CreateNotModifiedResponse(HttpRequestMessage request,
            EntityTagHeaderValue eTag,
            DateTimeOffset timeLastWrite)
        {
            var notModifiedResponse = request.CreateResponse(HttpStatusCode.NotModified);
            SetCaching(notModifiedResponse, eTag, timeLastWrite);
            return notModifiedResponse;
        }

        private static void SetCaching(HttpResponseMessage notModifiedResponse, EntityTagHeaderValue eTag,
            DateTimeOffset timeLastWriteOffset)
        {
            notModifiedResponse.Headers.AcceptRanges.Add(AcceptRangeBytes);
            notModifiedResponse.Headers.CacheControl = new CacheControlHeaderValue {Public = true};
            notModifiedResponse.Headers.ETag = eTag;
            notModifiedResponse.Content.Headers.LastModified = timeLastWriteOffset;
        }
    }
}