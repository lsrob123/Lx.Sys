using System;
using System.Collections.Generic;
using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Utilities.Contract.Web {
    public class UploadFileResultDto : IDto {
        public ICollection<string> FileNames { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedTimestamp { get; set; }
        public DateTimeOffset UpdatedTimestamp { get; set; }
        public string DownloadLink { get; set; }
        public ICollection<string> ContentTypes { get; set; }
        public ICollection<string> Names { get; set; }
    }
}