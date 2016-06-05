using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XDevMessaging.Infrastructure.PushNotifications
{
    public interface INotificationsChannelProvider
    {
        string GetChannel();
    }
}
