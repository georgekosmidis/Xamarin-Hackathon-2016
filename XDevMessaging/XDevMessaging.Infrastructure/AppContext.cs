using Newtonsoft.Json;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XDevMessaging.Infrastructure.Displaying;
using XDevMessaging.Infrastructure.Extensions;
using XDevMessaging.Infrastructure.Location;
using XDevMessaging.Infrastructure.Repositories;
using XDevMessaging.Infrastructure.Tables;

namespace XDevMessaging.Infrastructure
{
    public class AppContext
    {
        public IRepository Repository { get; protected set; }
        public IGeolocator GeoLocator { get; protected set; }
        public IPositionUpdater PositionUpdater { get; protected set; }
        public IDisplayNameProvider<User> UserDisplayNameProvider { get; protected set; }

        public MeetupRepository MeetupRepository { get;  protected set;}

        public Position CurrentPosition { get; protected set; }
        public User CurrentUser { get; protected set; }
        public bool IsInitialized { get; protected set; }

        public static bool IsChatting { get; protected set; }
        public static string ChatterId { get; protected set; }

        public AppContext(IRepository repository, IGeolocator geoLocator, IPositionUpdater positionUpdater, IDisplayNameProvider<User> userDisplayNameProvider, MeetupRepository meetupRepository)
        {
            this.Repository = repository;
            this.GeoLocator = geoLocator;

            this.PositionUpdater = positionUpdater;
            this.UserDisplayNameProvider = userDisplayNameProvider;
            this.MeetupRepository = meetupRepository;
        }

        public bool IsUserStored
        {
            get
            {
                return Application.Current.Properties.ContainsKey("loggedUser");
            }
        }

        public async Task LoginFromStorageAsync()
        {
            var loggedUser = Application.Current.Properties["loggedUser"] as string;
            User currentUser = JsonConvert.DeserializeObject<User>(loggedUser);
            string currentUserId = currentUser.Id;
            //reload from azure to update version
            //currentUser = (await Repository.GetAsync<User>(x => x.Id == currentUserId)).First();
            await InitializeLoginAsync(currentUser);
        }

        private volatile bool SendingPosition;

        public async Task InitializeLoginAsync(User currentUser)
        {
            this.CurrentUser = currentUser;

            if (GeoLocator.IsGeolocationAvailable == false)
            {
                //throw new Exception ???
            }
            if (GeoLocator.IsGeolocationEnabled == false)
            {
                //throw new Exception ???
            }

            if (this.GeoLocator.IsListening == false)
            {
                //this.CurrentPosition = await this.GeoLocator.GetPositionAsync(3000);
                await this.GeoLocator.StartListeningAsync(1000, 1);
            }

            this.GeoLocator.PositionChanged += async (sender, e) =>
            {
                if (!SendingPosition)
                {
                    SendingPosition = true;
                    this.CurrentPosition = e.Position;
                    await this.PositionUpdater.UpdateUserPositionAsync(this.CurrentUser, e.Position);
                    SendingPosition = false;
                }
            };

           

            //this.CurrentUser.NotificationId = PushNotification.Plugin.CrossPushNotification.Current.Token;
            this.IsInitialized = true;
        }


        public async Task LogoutAsync()
        {
            Application.Current.Properties.Remove("loggedUser");
            await Application.Current.SavePropertiesAsync();
            await this.GeoLocator.StopListeningAsync();
            this.CurrentUser = null;
            PinExtensions.CleanUp();
            this.IsInitialized = false;
        }
    }
}
