using System;
using System.IO;
using Lx.Membership.Contracts.Config;
using Lx.Utilities.Services.Config;

namespace Lx.Membership.Endpoint.Config
{
    public class PasswordResetEmailTemplates : IPasswordResetEmailTemplates
    {
        public string Url => this.AppSettingStringValue(x => x.Url);
        public string Subject => this.AppSettingStringValue(x => x.Subject);

        public string Body
        {
            get
            {
                var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates\\PasswordResetEmail.txt");
                var body = File.ReadAllText(filePath);
                return body;
            }
        }
    }
}