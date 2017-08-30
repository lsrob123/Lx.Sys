using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Utilities.Contract.Email
{
    public class EmailAttachmentDto : IEmailAttachment, IDto
    {
        public string FileName { get; set; }
    }
}