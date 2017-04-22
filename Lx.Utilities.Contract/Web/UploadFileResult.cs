using System;
using System.Collections.Generic;
using Lx.Utilities.Contract.Infrastructure.DTOs;
using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Utilities.Contract.Web
{
    public class UploadFileResult : ResultBase
    {
        public Guid UploadKey { get; set; }
        public string Description { get; set; }
        public DateTimeOffset TimeUploaded { get; set; }
        public ICollection<UploadFileInfoDto> UploadFiles { get; set; }

        public override void EraseShareGroupInfoForClientSide() { }
    }
}