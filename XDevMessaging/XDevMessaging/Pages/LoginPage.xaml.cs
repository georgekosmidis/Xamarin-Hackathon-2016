using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using XDevMessaging.Infrastructure;
using XDevMessaging.Infrastructure.Connectivity;
using XDevMessaging.Infrastructure.Repositories;
using XDevMessaging.Infrastructure.Tables;

namespace XDevMessaging.Pages
{
    public partial class LoginPage : ContentPage
    {
        AppContext AppContext { get; set; }

        public LoginPage(AppContext appContext)
        {
            InitializeComponent();
            AppContext = appContext;
        }
        private async void OnLoginBtnClicked(object sender, EventArgs args)
        {
            DisableUI(); // Disable UI
            var usernameText = UsernameEntry.Text;
            var passwordText = PasswordEntry.Text;
            if (!string.IsNullOrEmpty(usernameText) && !string.IsNullOrEmpty(passwordText))
            {
                ConnectivityHandler conHandler = new ConnectivityHandler();
                bool isConnected = await conHandler.IsConnected();
                if (isConnected)
                {
                    var users = await AppContext.Repository.GetAsync<User>(x => x.Username == usernameText);
                    if (users == null)
                    {
                        LoginCallbackLabel.Text = "Username doesn't exist";
                        EnableUI(); // Enable UI
                    }
                    else
                    {
                        var user = users.FirstOrDefault();
                        if (user == null)
                        {
                            LoginCallbackLabel.Text = "Username doesn't exist";
                            EnableUI(); // Enable UI
                        }
                        else
                        {
                            if (PasswordEntry.Text == user.Password)
                            {
                                LoginCallbackLabel.TextColor = Color.Green;
                                LoginCallbackLabel.Text = "Map page loading..";
                                //Set stored data
                                Application.Current.Properties["loggedUser"] = JsonConvert.SerializeObject(user);
                                await Application.Current.SavePropertiesAsync();
                                //GOTO: MapPage                         
                                Navigation.InsertPageBefore(new MapPage(AppContext), this);
                                Navigation.RemovePage(this);
                                //After login
                                await AppContext.InitializeLoginAsync(user);
                            }
                            else
                            {
                                LoginCallbackLabel.Text = "Wrong password";
                                EnableUI(); // Enable UI
                            }
                        }
                    }
                }
                else {
                    LoginCallbackLabel.Text = "No Internet Connection";
                    EnableUI(); // Enable UI
                }
        }
            else
            {
                LoginCallbackLabel.Text = "Username and password must be filled";
                EnableUI(); // Enable UI
            }
        }
        async void OnRegisterBtnClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new RegisterPage(AppContext));
        }
        void DisableUI() {
            ActIndicator.IsVisible = true;
            BoxIndicator.IsVisible = true;
            UsernameEntry.IsEnabled = false;
            PasswordEntry.IsEnabled = false;
            LoginButton.IsEnabled = false;
            RegisterButton.IsEnabled = false;
        }
        void EnableUI()
        {
            ActIndicator.IsVisible = false;
            BoxIndicator.IsVisible = false;
            UsernameEntry.IsEnabled = true;
            PasswordEntry.IsEnabled = true;
            LoginButton.IsEnabled = true;
            RegisterButton.IsEnabled = true;
        }
    }
}
