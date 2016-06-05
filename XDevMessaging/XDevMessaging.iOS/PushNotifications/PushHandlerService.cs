using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Gcm;
using WindowsAzure.Messaging;

[assembly: Permission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]
//GET_ACCOUNTS is only needed for android versions 4.0.3 and below
[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]
namespace XDevMessaging.Droid.PushNotifications
{
    [BroadcastReceiver(Permission = Gcm.Client.Constants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_MESSAGE },
        Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK },
        Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_LIBRARY_RETRY },
        Categories = new string[] { "@PACKAGE_NAME@" })]
    public class MyBroadcastReceiver : GcmBroadcastReceiverBase<PushHandlerService>
    {
        public static string[] SENDER_IDS = new string[] { PushNotifications.Constants.SenderID };

        public const string TAG = "MyBroadcastReceiver-GCM";
    }

    [Service] // Must use the service tag
    public class PushHandlerService : GcmServiceBase
    {
        public static string RegistrationID { get; private set; }
        private NotificationHub Hub { get; set; }

        public PushHandlerService()
            : base(PushNotifications.Constants.SenderID)
        {

        }

        protected override void OnError(Context context, string errorId)
        {

        }

        protected override void OnMessage(Context context, Intent intent)
        {
            var msg = new StringBuilder();

            if (intent != null && intent.Extras != null)
            {
                foreach (var key in intent.Extras.KeySet())
                    msg.AppendLine(key + "=" + intent.Extras.Get(key).ToString());
            }

            string messageText = intent.Extras.GetString("msg");
            string parameterText = intent.Extras.GetString("param");
            if (!string.IsNullOrEmpty(messageText))
            {
                CreateNotification("Awesome Notification...", messageText, parameterText);
                return;
            }

            // If the incoming message's parameters couldn't be recognized, then this Notification will be published...
            CreateNotification("Awesome Notification...", msg.ToString());
        }

        protected override void OnRegistered(Context context, string registrationId)
        {
            RegistrationID = registrationId;

            Hub = new NotificationHub(PushNotifications.Constants.NotificationHubPath, PushNotifications.Constants.ConnectionString, context);
            try
            {
                Hub.UnregisterAll(registrationId);
            }
            catch (Exception ex)
            {
                throw;
            }

            var tags = new List<string>() { "falcons" }; // create tags if you want

            try
            {
                var hubRegistration = Hub.Register(registrationId, tags.ToArray());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {

        }

        void CreateNotification(string title, string desc, string parameters = null)
        {
            Intent startupIntent = new Intent(this, typeof(MainActivity));

            startupIntent.PutExtra("param", parameters.ToString());

            TaskStackBuilder stackBuilder = TaskStackBuilder.Create(this);

            stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(MainActivity)));

            stackBuilder.AddNextIntent(startupIntent);


            const int pendingIntentId = 0;
            PendingIntent pendingIntent =
                stackBuilder.GetPendingIntent(pendingIntentId, PendingIntentFlags.OneShot);

            Notification.Builder builder = new Notification.Builder(this)
                .SetContentIntent(pendingIntent)
                .SetContentTitle(title)
                .SetContentText(desc)
                .SetSmallIcon(Resource.Drawable.icon);

            // Build the notification:
            Notification notification = builder.Build();
            notification.Flags = NotificationFlags.AutoCancel;

            // Get the notification manager:
            NotificationManager notificationManager =
                GetSystemService(Context.NotificationService) as NotificationManager;

            // Publish the notification:
            const int notificationId = 0;
            notificationManager.Notify(notificationId, notification);
        }
    }
}