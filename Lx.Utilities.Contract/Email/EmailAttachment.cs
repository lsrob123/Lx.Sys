using System.ComponentModel.DataAnnotations;
using Lx.Utilities.Contract.Infrastructure.Domain;

namespace Lx.Utilities.Contract.Email
{
    public class EmailAttachment : RelatedValueObjectBase, IEmailAttachment
    {
        [StringLength(500)]
        public string FileName { get; protected set; }
    }
}