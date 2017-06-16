using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lx.Utilities.Contracts.Email;
using Lx.Utilities.Contracts.Infrastructure.Enumerations;
using Lx.Utilities.Contracts.Infrastructure.Extensions;
using Lx.Utilities.Contracts.Infrastructure.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;

namespace Lx.Utilities.Services.Email
{
    public class MimeKitEmailSender : IEmailSender
    {
        private readonly IEmailSettings _emailSettings;
        private readonly ISmtpSettings _settings;

        public MimeKitEmailSender(ISmtpSettings settings, IEmailSettings emailSettings)
        {
            _settings = settings;
            _emailSettings = emailSettings;
        }

        public Task<SendEmailResponse> SendEmailAsync(SendEmailRequest request, int interval,
            IProgressReporter<SendEmailProgress> progressReporter)
        {
            return SendEmailAsync(request.Sender, request.To, request.Cc, request.Bcc, interval, request.Subject,
                request.Content, request.IsHtml, request.Attachments, progressReporter);
        }

        public async Task<SendEmailResponse> SendEmailAsync(EmailParticipant sender, IEnumerable<EmailParticipant> to,
            IEnumerable<EmailParticipant> cc, IEnumerable<EmailParticipant> bcc, int interval, string subject,
            string content, bool isHtml, IEnumerable<EmailAttachment> attachments,
            IProgressReporter<SendEmailProgress> progressReporter)
        {
            var attachmentList = attachments.ToList();
            var toList = to.ToList();
            var toCount = toList.Count;
            var ccList = cc.ToList();
            var bccList = bcc.ToList();

            if (_emailSettings.DumpToFilesOnly)
            {
                var filePath = Path.Combine(_emailSettings.DumpFileFolder,
                    DateTimeOffset.UtcNow.ToString("yyyy-MM-dd HHmmss") + subject);
                var dumpContentBuilder = new StringBuilder();
                dumpContentBuilder.AppendLine(subject);

                if (!string.IsNullOrWhiteSpace(sender?.EmailAddress))
                    dumpContentBuilder.AppendLine("From: " + sender.EmailAddress);

                foreach (var ccItem in ccList)
                    dumpContentBuilder.AppendLine("To: " + ccItem.EmailAddress);

                dumpContentBuilder.AppendLine(content);
                File.WriteAllText(filePath, dumpContentBuilder.ToString());
                return new SendEmailResponse().WithProcessResult(ProcessResultType.Ok);
            }

            progressReporter?.SetTotal(toCount, null, $"Sending {toCount} email(s) to SMTP server...",
                DateTimeOffset.UtcNow);
            if (interval <= 0) //don't need to wait, just send them all in one go
            {
                var res = await SendEmailAsync(sender, toList, ccList, bccList, subject, content, isHtml,
                    attachmentList);
                progressReporter?.Report(toList.Count, null, $"Sent {toCount} email(s) to SMTP server.",
                    DateTimeOffset.UtcNow);
                return res;
            }
            else //send email at given interval 
            {
                var subreslst = new List<SendEmailResponse>();
                foreach (var recipient in toList)
                {
                    var subres = await SendEmailAsync(sender,
                        new List<EmailParticipant> {recipient}, ccList, bccList, subject, content, isHtml,
                        attachmentList);
                    subreslst.Add(subres);
                    progressReporter?.Report(1, null,
                        $"Sent email to SMTP server: {recipient.EmailAddress} ({recipient.Name})",
                        DateTimeOffset.UtcNow);
                    await Task.Delay(interval);
                }
                var res = CombineMultipleSendEmailResponses(subreslst);
                return res;
            }
        }

        public static SendEmailResponse CombineMultipleSendEmailResponses(
            IReadOnlyCollection<SendEmailResponse> subreslst)
        {
            //simply take the first result when there is only one result
            if (subreslst.Count == 1)
                return subreslst.First();
            //otherwise, we need to combine the results from multiple results
            var status = ProcessResultType.Ok;
            var exs = new List<Exception>();
            foreach (var subres in subreslst)
            {
                if (!Equals(subres.Result.Type, ProcessResultType.Ok)) status = ProcessResultType.MultiStatus;
                if (subres.Result?.Exceptions != null) exs.AddRange(subres.Result.Exceptions);
            }
            var res = new SendEmailResponse {Result = status};
            res.Result.SetExceptions(exs.Any() ? exs : null);
            return res;
        }


        private async Task<SendEmailResponse> SendEmailAsync(EmailParticipant sender, ICollection<EmailParticipant> to,
            ICollection<EmailParticipant> cc, ICollection<EmailParticipant> bcc, string subject, string content,
            bool isHtml, IEnumerable<EmailAttachment> attachments)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    client.Connect(_settings.Host, _settings.Port, _settings.IsSsl);
                    // Note: since we don't have an OAuth2 token, disable
                    // the XOAUTH2 authentication mechanism.
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    // Note: only needed if the SMTP server requires authentication
                    var username = _settings.Username;
                    var password = _settings.Password;
                    if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
                        client.Authenticate(username, password);

                    var message = ComposeEmail(sender, to, cc, bcc, subject, content, isHtml, attachments);
                    await client.SendAsync(message);
                    client.Disconnect(true);
                    return new SendEmailResponse {Result = ProcessResultType.Ok};
                }
            }
            catch (Exception ex)
            {
                return new SendEmailResponse {Result = ex};
            }
        }

        private static string DetectFileMimeType(string str)
        {
            if (str.EndsWith(".zip")) return "application/zip";
            if (str.EndsWith(".rar")) return "application/x-rar-compressed";

            if (str.EndsWith(".bmp")) return "image/bmp";
            if (str.EndsWith(".png")) return "image/png";
            if (str.EndsWith(".gif")) return "image/gif";
            if (str.EndsWith(".jpg")) return "image/jpeg";
            if (str.EndsWith(".jpeg")) return "image/jpeg";

            if (str.EndsWith(".avi")) return "video/avi";
            if (str.EndsWith(".flv")) return "video/x-flv";
            if (str.EndsWith(".mp4")) return "video/mp4";
            if (str.EndsWith(".m3u8")) return "application/x-mpegURL";
            if (str.EndsWith(".mov")) return "video/quicktime";

            if (str.EndsWith(".wav")) return "audio/wav";
            if (str.EndsWith(".mp3")) return "audio/mpeg3";

            if (str.EndsWith(".dwf")) return "model/vnd.dwf";
            if (str.EndsWith(".dwg")) return "application/acad";
            if (str.EndsWith(".pdf")) return "application/pdf";

            if (str.EndsWith(".doc")) return "application/msword";
            if (str.EndsWith(".dot")) return "application/msword";

            if (str.EndsWith(".docx")) return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            if (str.EndsWith(".dotx")) return "application/vnd.openxmlformats-officedocument.wordprocessingml.template";
            if (str.EndsWith(".docm")) return "application/vnd.ms-word.document.macroEnabled.12";
            if (str.EndsWith(".dotm")) return "application/vnd.ms-word.template.macroEnabled.12";

            if (str.EndsWith(".xls")) return "application/vnd.ms-excel";
            if (str.EndsWith(".xlt")) return "application/vnd.ms-excel";
            if (str.EndsWith(".xla")) return "application/vnd.ms-excel";

            if (str.EndsWith(".xlsx")) return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            if (str.EndsWith(".xltx")) return "application/vnd.openxmlformats-officedocument.spreadsheetml.template";
            if (str.EndsWith(".xlsm")) return "application/vnd.ms-excel.sheet.macroEnabled.12";
            if (str.EndsWith(".xltm")) return "application/vnd.ms-excel.template.macroEnabled.12";
            if (str.EndsWith(".xlam")) return "application/vnd.ms-excel.addin.macroEnabled.12";
            if (str.EndsWith(".xlsb")) return "application/vnd.ms-excel.sheet.binary.macroEnabled.12";

            if (str.EndsWith(".ppt")) return "application/vnd.ms-powerpoint";
            if (str.EndsWith(".pot")) return "application/vnd.ms-powerpoint";
            if (str.EndsWith(".pps")) return "application/vnd.ms-powerpoint";
            if (str.EndsWith(".ppa")) return "application/vnd.ms-powerpoint";

            if (str.EndsWith(".pptx"))
                return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
            if (str.EndsWith(".potx")) return "application/vnd.openxmlformats-officedocument.presentationml.template";
            if (str.EndsWith(".ppsx")) return "application/vnd.openxmlformats-officedocument.presentationml.slideshow";
            if (str.EndsWith(".ppam")) return "application/vnd.ms-powerpoint.addin.macroEnabled.12";
            if (str.EndsWith(".pptm")) return "application/vnd.ms-powerpoint.presentation.macroEnabled.12";
            if (str.EndsWith(".potm")) return "application/vnd.ms-powerpoint.template.macroEnabled.12";
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (str.EndsWith(".ppsm")) return "application/vnd.ms-powerpoint.slideshow.macroEnabled.12";

            return "application/octet-stream";
        }

        private static MimeMessage ComposeEmail(EmailParticipant sender, IEnumerable<EmailParticipant> to,
            ICollection<EmailParticipant> cc, ICollection<EmailParticipant> bcc, string subject, string content,
            bool isHtml = false, IEnumerable<EmailAttachment> attachments = null)
        {
            var message = new MimeMessage(
                new[] {new MailboxAddress(sender.Name, sender.EmailAddress)},
                to.Where(x => !string.IsNullOrWhiteSpace(x.EmailAddress))
                    .Select(x => new MailboxAddress(x.Name, x.EmailAddress))
                    .ToList(), //key is name, value is address
                subject,
                null
            );
            if (cc != null && cc.Any())
                cc.ToList()
                    .ForEach(x =>
                    {
                        if (!string.IsNullOrWhiteSpace(x.EmailAddress))
                            message.Cc.Add(new MailboxAddress(x.Name, x.EmailAddress));
                    }); //key is name, value is address

            if (bcc != null && bcc.Any())
                bcc.ToList()
                    .ForEach(x =>
                    {
                        if (!string.IsNullOrWhiteSpace(x.EmailAddress))
                            message.Bcc.Add(new MailboxAddress(x.Name, x.EmailAddress));
                    }); //key is name, value is address
            var body = new TextPart(isHtml ? "html" : "plain")
            {
                Text = content
            };

            var attachmentList = attachments?.ToList();
            if (attachmentList != null && attachmentList.Any())
            {
                var multipart = new Multipart("mixed") {body};
                attachmentList.ForEach(c =>
                {
                    var mimetype = DetectFileMimeType(c.FileName);
                    var attachment = new MimePart(mimetype.Split('/')[0], mimetype.Split('/')[1])
                    {
                        ContentObject =
                            new ContentObject(File.OpenRead(c.FileName), ContentEncoding.Default),
                        ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                        ContentTransferEncoding = ContentEncoding.Base64,
                        FileName = Path.GetFileName(c.FileName)
                    };
                    multipart.Add(attachment);
                });
                message.Body = multipart;
            }
            else
            {
                message.Body = body;
            }

            return message;
        }
    }
}