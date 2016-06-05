using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Connectivity;

namespace XDevMessaging.Infrastructure.Connectivity
{
    public class ConnectivityHandler
    {
        public ConnectivityHandler()
        {
        }
        public async Task<bool> IsConnected()
        {
            return CrossConnectivity.Current.IsConnected &&
                   await CrossConnectivity.Current.IsRemoteReachable("google.com");
        }
    }
}
