using System.Collections.Generic;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.StaticFiles.ContentTypes;

namespace Lx.Utilities.Services.OWIN {
    public static class StaticFileServerOptionsExtensions {
        public static FileServerOptions WithFolderAndDefaultFile(this FileServerOptions options, string folderName,
            string defaultFileName = "index.html") {
            options.EnableDirectoryBrowsing = false;
            options.EnableDefaultFiles = true;
            options.DefaultFilesOptions.DefaultFileNames = new List<string> {"index.html"};

            options.FileSystem = new PhysicalFileSystem("Assets");
            options.StaticFileOptions.ContentTypeProvider = new FileExtensionContentTypeProvider();

            return options;
        }
    }
}