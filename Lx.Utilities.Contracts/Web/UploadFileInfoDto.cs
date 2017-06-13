using System.IO;
using System.Runtime.Serialization;
using Lx.Utilities.Contracts.Infrastructure.Interfaces;
using Newtonsoft.Json;

namespace Lx.Utilities.Contracts.Web
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