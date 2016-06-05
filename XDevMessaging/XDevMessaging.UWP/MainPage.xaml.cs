using Microsoft.WindowsAzure.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.PushNotifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace XDevMessaging.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            
            Xamarin.FormsMaps.Init("PUT_YOUR_KEY_HERE");

            this.InitializeComponent();

            PushNotification.Plugin.CrossPushNotification.Initialize<Notifications.CrossPushNotificationListener>();
            PushNotification.Plugin.CrossPushNotification.Current.Register();
            //var o = PushNotification.Plugin.CrossPushNotification.Current.Token;

            //XDevMessaging.Notifications.CrossPushNotificationSender.PushNotification(o, "test", "test");
            LoadApplication(new XDevMessaging.App());
          
        }



    }




}
