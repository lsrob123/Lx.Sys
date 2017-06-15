using Lx.Communications.Services.Porcesses;
using Lx.Utilities.Contracts.Email;
using Lx.Utilities.Contracts.IoC;
using Lx.Utilities.Services.Email;

namespace Lx.Communications.Services.Dependencies
{
    public class Register : DefaultDependencyRegisterBase
    {
        public override void AddRegistrations()
        {
            Register<IEmailSender, MimeKitEmailSender>();

            Register<IEmailService, EmailService>();
        }
    }
}