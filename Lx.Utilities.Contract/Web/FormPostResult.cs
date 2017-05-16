using System.Collections.Generic;
using System.Collections.Specialized;
using Lx.Utilities.Contract.Infrastructure.DTOs;

namespace Lx.Utilities.Contract.Web
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