using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Lx.Utilities.Contract.Infrastructure.Interfaces;
using Newtonsoft.Json;

namespace Lx.Utilities.Contract.Web
{
    public class UploadFileInfoDto : IDto
    {
        [IgnoreDataMember]
        [JsonIgnore]
        public string TempFileName { get; set; }

        public string FileName { get; set; }
        public string FileNameExtension => Path.GetExtension(FileName);
        public string FullFilePath { get; set; }
        public string MediaType { get; set; }
    }
}
