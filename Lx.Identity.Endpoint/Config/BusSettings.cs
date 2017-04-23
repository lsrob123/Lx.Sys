using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lx.Utilities.Contract.ServiceBus;
using Lx.Utilities.Services.Config;
using Lx.Utilities.Services.ServiceBus.Nsb;

namespace Lx.Identity.Endpoint.Config
{
   public class BusSettings: NsbBusSettingsBase
   {
       public override string EndpointName => this.AppSettingStringValue(x => x.EndpointName);
   }
}
