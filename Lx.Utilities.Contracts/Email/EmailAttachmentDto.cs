using Lx.Utilities.Contracts.Infrastructure.Interfaces;

namespace Lx.Utilities.Contracts.Email
{
    public class EmailAttachmentDto : IEmailAttachment, IDto
    {
        public string FileName { get; set; }
    }
}