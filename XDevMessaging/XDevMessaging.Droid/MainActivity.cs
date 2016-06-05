using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace XDevMessaging.Droid
{
    [Activity(Label = "MeetADev", Icon = "@drawable/icon", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, Theme = "@android:style/Theme.Holo.Light")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.FormsMaps.Init(this,bundle);

            //PushNotification.Plugin.CrossPushNotification.Initialize<Notifications.CrossPushNotificationListener>(Infractructure.PushNotifications.Constants.SenderID);
            //PushNotification.Plugin.CrossPushNotification.Current.Register();

            string parameterValue = this.Intent.GetStringExtra("param");
            //AzurePushNotificationImplementation.MainActivityInstance = this;
            //AzurePushNotificationImplementation.NotificationIconDrawable = Android.Resource.Drawable.StatNotifyMore;

            if (parameterValue != null)
            {
                LoadApplication(new App(parameterValue));
            }
            else
            {
                LoadApplication(new App());
            }

            //XDevMessaging.App.AppContext.CurrentUser.NotificationId = PushNotification.Plugin.CrossPushNotification.Current.Token;
            //XDevMessaging.App.AppContext.Repository.UpdateAsync(XDevMessaging.App.AppContext.CurrentUser);
            //RegisterWithGCM();
        }

        //private void RegisterWithGCM()
        //{
        //    // Check to ensure everything's setup right
        //    GcmClient.CheckDevice(this);
        //    GcmClient.CheckManifest(this);

        //    // Register for push notifications
        //    GcmClient.Register(this, PushNotifications.Constants.SenderID);
        //}
    }
}

