using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Lx.Utilities.Contracts.Web;
using RazorTemplates.Core;

namespace Lx.Utilities.Services.Web
{
    public class RazorTemplatesService : ITypedModelHtmlGenerationService
    {
        protected static volatile bool AllTemplatesCompiled;

        protected static readonly ConcurrentDictionary<Type, ITemplate<dynamic>> CompiledTemplates =
            new ConcurrentDictionary<Type, ITemplate<dynamic>>();

        public RazorTemplatesService()
        {
            if (AllTemplatesCompiled)
                return;

            Compile(RazorTemplateLoader.Templates);
            AllTemplatesCompiled = true;
        }

        public string GetHtml<TModel>(TModel model)
        {
            var type = model.GetType();

            ITemplate<dynamic> compiledTemplate;
            if (!CompiledTemplates.TryGetValue(type, out compiledTemplate))
                return null;

            var html = compiledTemplate.Render(model);
            return html;
        }

        protected void Compile(IDictionary<Type, string> templates)
        {
            foreach (var template in templates)
            {
                var templateWithoutNamespaceImport = Regex.Replace(template.Value, $"@model.*{template.Key.FullName}",
                    string.Empty);

                var compiledTemplate = Template.WithBaseType<TemplateBase>()
                    .AddNamespace(template.Key.Namespace)
                    .Compile(templateWithoutNamespaceImport);

                if (compiledTemplate != null)
                    CompiledTemplates.TryAdd(template.Key, compiledTemplate);
            }
        }
    }
}