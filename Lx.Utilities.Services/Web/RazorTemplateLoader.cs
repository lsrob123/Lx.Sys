using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using Lx.Utilities.Contract.Configuration;
using Lx.Utilities.Services.Infrastructure;

namespace Lx.Utilities.Services.Web {
    public class RazorTemplateLoader {
        public static ConcurrentDictionary<Type, string> Templates { get; protected set; }

        [Preconfiguration]
        public static void LoadAndCompileRazorTemplates() {
            Templates = new ConcurrentDictionary<Type, string>();

            string[] files;

            try {
                files = Directory.GetFileSystemEntries(Directory.GetCurrentDirectory(), "*.cshtml",
                    SearchOption.AllDirectories);
            } catch (Exception ex) {
                var exceptionString = ex.ToString();
                Console.WriteLine(exceptionString);
                return;
            }

            if (!files.Any())
                return;

            var types = AssemblyHelper.GetReferencedAssemblies().SelectMany(a => a.GetTypes()).ToList();
            foreach (var file in files) {
                var fileName = Path.GetFileNameWithoutExtension(file);
                var modelType = types.FirstOrDefault(x => x.Name.Equals(fileName, StringComparison.OrdinalIgnoreCase));
                if (modelType != null)
                    Templates.TryAdd(modelType, File.ReadAllText(file));
            }
        }
    }
}