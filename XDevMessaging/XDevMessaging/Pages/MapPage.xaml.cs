using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using XDevMessaging.Infrastructure;
using XDevMessaging.Infrastructure.Location;
using XDevMessaging.Infrastructure.Repositories;
using XDevMessaging.Infrastructure.Extensions;
using XDevMessaging.Infrastructure.Tables;
using System.Globalization;
using XDevMessaging.Maps;

namespace XDevMessaging.Pages
{
    public partial class MapPage : ContentPage
    {
        private AppContext AppContext { get; set; }

        public MapPage(AppContext appContext)
        {
            this.AppContext = appContext;
            //var existingPages = Navigation.NavigationStack.ToList();
            //foreach (var page in existingPages)
            //{
            //if(page!=this)
            //Navigation.RemovePage(page);
            //}
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();            
            await SetPins();
        }

        private async Task SetPins()
        {
            var users = await AppContext.Repository.GetAsync<User>(x => x.Id != null && x.Id != AppContext.CurrentUser.Id && x.Latitude != null && x.Longitude != null);
            var userList = users.ToList();
            
            MyMap.Pins.Clear();

            foreach (var user in userList)
            {
                var position = new Xamarin.Forms.Maps.Position(
                    double.Parse(user.Latitude,CultureInfo.InvariantCulture), 
                    double.Parse(user.Longitude,CultureInfo.InvariantCulture)); // Latitude, Longitude
                
                var xDevPin = new Pin();

                xDevPin.Type = PinType.Generic;
                xDevPin.Position = position;
                xDevPin.Label = string.Format("{0} speaks {1}", user.Username, user.CodingLanguage);
                xDevPin.Address = "Click 2 Chat";

                xDevPin.SetUser(user);
                xDevPin.Clicked += OnUserPinBtnClicked;
                MyMap.Pins.Add(xDevPin);

            }
            if (AppContext.CurrentUser.Latitude == null)
                return;
            var eventList = await AppContext.MeetupRepository.GetLocalGroups(AppContext.CurrentUser.Latitude, AppContext.CurrentUser.Longitude);

            foreach (var eventItem in eventList)
            {
                var position = new Xamarin.Forms.Maps.Position(
                    double.Parse(eventItem.Venue.Lat, CultureInfo.InvariantCulture),
                    double.Parse(eventItem.Venue.Lon, CultureInfo.InvariantCulture));

                var xDevPin = new Pin();

                xDevPin.Type = PinType.Place;
                xDevPin.Position = position;
                xDevPin.Label = eventItem.Name;
                xDevPin.Address = eventItem.Venue.Address;

                Uri eventUri = new Uri(eventItem.EventUrl);
                xDevPin.Clicked += delegate (object sender, EventArgs args)
                    { OnMeetupPinBtnClicked(sender, args, eventUri); };

                MyMap.Pins.Add(xDevPin);

            }

        }


        private async void OnUserPinBtnClicked(object sender, EventArgs args)
        {
            Pin clickedPin = sender as Pin;
            User user = clickedPin.GetUser();
            string userDisplayName = AppContext.UserDisplayNameProvider.GetDisplayName(user);
            var answer = await DisplayAlert ("Dev Chat", string.Format("Do you want to chat with user {0}\"{1}\"?", Environment.NewLine, userDisplayName), "Yes", "No");
            
            if (answer)
            {
                //todo change params
                await Navigation.PushAsync(new ChatPage(AppContext, user));
            }

        }
        private async void OnMeetupPinBtnClicked(object sender, EventArgs args,Uri meetupUri)
        {
            Pin clickedPin = sender as Pin;

            var answer = await DisplayAlert ("Dev Event", string.Format("Do you want to open the event?"), "Yes", "No");
            
            if (answer)
            {
                //todo change params
                Device.OpenUri(meetupUri);
            }
           
        }


        async void OnToolbarItemClick(object sender, EventArgs e)
        {
            //Remove stored data
            await AppContext.LogoutAsync();
            Navigation.InsertPageBefore(new LoginPage(AppContext), this);
            Navigation.RemovePage(this);
        }
    }
}
