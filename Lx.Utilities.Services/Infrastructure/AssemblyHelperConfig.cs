using System;
using System.Collections.Generic;
using Lx.Utilities.Services.Config;

namespace Lx.Utilities.Services.Infrastructure
{
    public class AssemblyHelperConfig
    {
        public ICollection<string> NamespaceKeywords
        {
            get
            {
                var keywords = new List<string>
                {
                    typeof(AssemblyHelper).Namespace?.Split('.')[0]
                };

                var keywordsInConfigFile =
                    this.AppSettingStringValue(x => x.NamespaceKeywords)?
                        .Split(new[] {" ", ",", ";"}, StringSplitOptions.RemoveEmptyEntries) ?? new string[] { };
                keywords.AddRange(keywordsInConfigFile);
                return keywords;
            }
        }
    }
}