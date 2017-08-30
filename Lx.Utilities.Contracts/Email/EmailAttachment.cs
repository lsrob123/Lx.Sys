using System.ComponentModel.DataAnnotations;
using Lx.Utilities.Contracts.Infrastructure.Domain;

namespace Lx.Utilities.Contracts.Email
{
    public class EmailAttachment : RelatedValueObjectBase, IEmailAttachment
    {
        [StringLength(500)]
        public string FileName { get; protected set; }
    }
}