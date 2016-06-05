using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XDevMessaging.Infractructure.PushNotifications
{
    public class Constants
    {
        public const string SenderID = "PUT_GOOGLE_API_PROJECT_NUMBER"; // Google API Project Number

        // Azure app specific connection string and hub path
        public const string ConnectionString = "PUT_CONNECTION_STRING_HERE";
        public const string NotificationHubPath = "XDevMessaging";
    }
}