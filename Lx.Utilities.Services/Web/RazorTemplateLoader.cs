using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using Lx.Utilities.Contract.Configuration;
using Lx.Utilities.Contract.Infrastructure.Common;
using Lx.Utilities.Services.Infrastructure;

namespace Lx.Utilities.Services.Web
{
    public class RazorTemplateLoader
    {
        public static ConcurrentDictionary<Type, string> Templates { get; protected set; }

        [Preconfiguration]
        public static void LoadAndCompileRazorTemplates()
        {
            Templates = new ConcurrentDictionary<Type, string>();

            var files = Directory.GetFileSystemEntries(Directory.GetCurrentDirectory(), "*.cshtml",
                SearchOption.AllDirectories);
            if (!files.Any())
                return;

            var types = AssemblyHelper.GetTypesInReferencedAssemblies();
            foreach (var file in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(file);
                var modelType = types.FirstOrDefault(x => x.Name.Equals(fileName, StringComparison.OrdinalIgnoreCase));
                if (modelType != null)
                    Templates.TryAdd(modelType, File.ReadAllText(file));
            }
        }
    }
}