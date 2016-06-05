using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XDevMessaging.Infrastructure;
using XDevMessaging.Infrastructure.Location;
using XDevMessaging.Infrastructure.Repositories;
using XDevMessaging.Infrastructure.Tables;
using XDevMessaging.Pages;
using Newtonsoft.Json;
using WindowsAzure.Messaging;
using XDevMessaging.Infrastructure.Displaying;

namespace XDevMessaging
{
    public class App : Application
    {

        public static AppContext AppContext;

        public App(string pushNotifParameter = null)
        {

            //INITIALIZATION
            if (AppContext == null)
            {
                IGeolocator geoLocator = CrossGeolocator.Current;
                IRepository repository = new AzureRepository("http://xdevmessaging.azurewebsites.net/");
                IPositionUpdater positionUpdater = new PositionUpdater(repository);
                IDisplayNameProvider<User> userDisplayNameProvider = new UserDisplayNameProvider();
                MeetupRepository meetupRepository= new MeetupRepository();
                AppContext = new AppContext(repository, geoLocator, positionUpdater, userDisplayNameProvider,meetupRepository);
            }

            //Check stored data
            //if (AppContext.IsUserStored)
            //{
            //    AppContext.LoginFromStorageAsync().Wait();
            //    var mapPage = new NavigationPage(new MapPage(AppContext));
            //    MainPage = mapPage;
            //}
            //else
            //{
                var loginPage = new NavigationPage(new LoginPage(AppContext));
                MainPage = loginPage;
            //}
        }


        protected async override void OnStart()
        {
          // var o = PushNotification.Plugin.CrossPushNotification.Current.Token;
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
