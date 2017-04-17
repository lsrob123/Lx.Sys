using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lx.Identity.Contracts.DTOs;
using Lx.Shared.All.Identity.Interfaces;

namespace Lx.Identity.Services.Services
{
 public   class ClientService:IClientService
    {
     public ClientDto GetClientByClientId(string clientId)
     {
     }

     public IHasUserProfileOriginator GetAssociatedUserProfileOriginator(string clientId)
     {
     }
    }
}
