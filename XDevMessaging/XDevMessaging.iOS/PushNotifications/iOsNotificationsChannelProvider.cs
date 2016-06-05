using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDevMessaging.Infrastructure.PushNotifications;
using XDevMessaging.iOS.PushNotifications;

[assembly: Xamarin.Forms.Dependency(typeof(iOSNotificationsChannelProvider))]

namespace XDevMessaging.iOS.PushNotifications
{
    public class iOSNotificationsChannelProvider : INotificationsChannelProvider
    {
        public string GetChannel()
        {
            return PushNotification.Plugin.CrossPushNotification.Current.Token;
            //XDevMessaging.App.AppContext.CurrentUser.NotificationId = PushNotification.Plugin.CrossPushNotification.Current.Token;
            //XDevMessaging.App.AppContext.Repository.UpdateAsync(XDevMessaging.App.AppContext.CurrentUser);
        }
    }
}
