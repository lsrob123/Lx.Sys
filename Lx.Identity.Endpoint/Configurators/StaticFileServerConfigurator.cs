using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.StaticFiles.ContentTypes;

namespace Lx.Identity.Endpoint.Configurators {
    public static class StaticFileServerConfigurator {
        public static FileServerOptions FileServerOptions() {
            var options = new FileServerOptions {
                EnableDirectoryBrowsing = false,
                EnableDefaultFiles = true,
                DefaultFilesOptions = {DefaultFileNames = {"index.html"}},
                FileSystem = new PhysicalFileSystem("StaticPages"),
                StaticFileOptions = {
                    ContentTypeProvider = new FileExtensionContentTypeProvider()
                }
            };

            return options;
        }
    }
}