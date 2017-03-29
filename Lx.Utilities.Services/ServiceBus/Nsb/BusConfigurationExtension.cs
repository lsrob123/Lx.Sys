using Autofac;
using NServiceBus;

namespace Lx.Utilities.Services.ServiceBus.Nsb {
    public static class BusConfigurationExtension {
        public static void RegisterWithAutofac(this BusConfiguration busConfiguration, IContainer container = null) {
            if (container == null)
                busConfiguration.UseContainer<AutofacBuilder>();
            else
                busConfiguration.UseContainer<AutofacBuilder>(
                    customizations => customizations.ExistingLifetimeScope(container));
        }
    }
}