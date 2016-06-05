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
    public partial class RegisterPage : ContentPage
    {
        List<string> items = new List<string>{
            "C#","C++","C","Cobol","Objective-C","Java","Python","JavaScript","PHP","Ruby","Delphi",
            "Perl","Scala","Swift","Pascal","Go","Groovy","R","Prolog"
        };
        private IEnumerable<User> users;

        private AppContext AppContext { get; set; }

        public RegisterPage(AppContext appContext)
        {
            AppContext = appContext;
            InitializeComponent();
            items.Sort();
            foreach (string item in items)
            {
                LanguagePicker.Items.Add(item);
            }
            LanguagePicker.SelectedIndex = 0;
        }
        private async void OnSubmitBtnClicked(object sender, EventArgs args)
        {
            DisableUI(); // Disable UI
            var usernameText = UsernameEntry.Text;
            var passwordText = PasswordEntry.Text;
            var confirmText = ConfirmEntry.Text;
            var language = items[LanguagePicker.SelectedIndex];
            if (!string.IsNullOrEmpty(usernameText) &&
                !string.IsNullOrEmpty(passwordText) &&
                passwordText == confirmText &&
                !string.IsNullOrEmpty(language)) {
                ConnectivityHandler conHandler = new ConnectivityHandler();
                bool isConnected = await conHandler.IsConnected();
                if (isConnected)
                {
                    var users = await AppContext.Repository.GetAsync<User>(x => x.Username == usernameText);
                    bool userExists = false;
                    if (users != null)
                    {
                        var oldUser = users.FirstOrDefault();
                        if (oldUser != null)
                        {
                            userExists = true;
                        }
                    }
                    if (userExists)
                    {
                        RegisterCallbackLabel.Text = "User named " + usernameText + " exists";
                        EnableUI(); // Enable UI
                    }
                    else
                    {
                        var newUser = new User();
                        newUser.Username = usernameText;
                        newUser.Password = passwordText;
                        newUser.CodingLanguage = language;
                        try
                        {
                            await AppContext.Repository.InsertAsync(newUser);
                            RegisterCallbackLabel.TextColor = Color.Green;
                            RegisterCallbackLabel.Text = "User has been registered successfully";
                            EnableUI(); // Enable UI
                        }
                        catch (Exception ex)
                        {
                            RegisterCallbackLabel.Text = "User Insert Error: " + ex;
                            EnableUI(); // Enable UI
                        }
                    }
                }
                else
                {
                    RegisterCallbackLabel.Text = "No Internet Connection";
                    EnableUI(); // Enable UI
                }
            }
            else
            {
                RegisterCallbackLabel.Text = "Empty or Invalid Inputs";
                EnableUI(); // Enable UI
            }
        }
        void DisableUI()
        {
            ActIndicator.IsVisible = true;
            BoxIndicator.IsVisible = true;
            UsernameEntry.IsEnabled = false;
            PasswordEntry.IsEnabled = false;
            ConfirmEntry.IsEnabled = false;
            LanguagePicker.IsEnabled = false;
            SubmitButton.IsEnabled = false;

        }
        void EnableUI()
        {
            ActIndicator.IsVisible = false;
            BoxIndicator.IsVisible = false;
            UsernameEntry.IsEnabled = true;
            PasswordEntry.IsEnabled = true;
            ConfirmEntry.IsEnabled = true;
            LanguagePicker.IsEnabled = true;
            SubmitButton.IsEnabled = true;
            
        }
    }
}
