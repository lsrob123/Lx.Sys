using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Lx.Utilities.Services.Networking {
    public static class NetworkingHelper {
        public static IPAddress GetLocalIpAddress() {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            var ip = host.AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
            return ip;
        }
    }
}