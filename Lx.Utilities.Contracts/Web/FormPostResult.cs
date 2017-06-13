using System.Collections.Generic;
using System.Collections.Specialized;
using Lx.Utilities.Contracts.Infrastructure.DTOs;

namespace Lx.Utilities.Contracts.Web
{
    public class FormPostResult : ResultBase
    {
        public NameValueCollection Fields { get; set; }
        public ICollection<UploadFileInfoDto> Files { get; set; }

        public override void EraseShareGroupInfoForClientSide()
        {
        }
    }
}