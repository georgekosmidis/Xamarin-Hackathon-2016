using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDevMessaging.Infrastructure.PushNotifications;
using XDevMessaging.Droid.PushNotifications;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidNotificationsChannelProvider))]

namespace XDevMessaging.Droid.PushNotifications
{
    public class AndroidNotificationsChannelProvider : INotificationsChannelProvider
    {
        public string GetChannel()
        {
            return PushNotification.Plugin.CrossPushNotification.Current.Token;
        }
    }
}
